using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using static USCS.Startup;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace USCS.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }


    public class AdAuthenticationService
    {
        private UserManagerContext _context = new UserManagerContext();

        public class AuthenticationResult
        {
            public AuthenticationResult(string errorMessage = null)
            {
                ErrorMessage = errorMessage;
            }

            public String ErrorMessage { get; private set; }
            public Boolean IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        }
        private readonly IAuthenticationManager authenticationManager;

        public AdAuthenticationService(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }
        public AuthenticationResult SignIn(String username, String password)
        {

            ContextType authenticationType = ContextType.Domain;

            PrincipalContext principalContext = new PrincipalContext(authenticationType);
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            try
            {
                isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                if (isAuthenticated)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                }
            }
            catch (Exception)
            {
                isAuthenticated = false;
                userPrincipal = null;
            }

            if (!isAuthenticated || userPrincipal == null)
            {
                return new AuthenticationResult("Username or Password is not correct");
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                // here can be a security related discussion weather it is worth 
                // revealing this information
                return new AuthenticationResult("Your account is locked.");
            }

            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                // here can be a security related discussion weather it is worth 
                // revealing this information
                return new AuthenticationResult("Your account is disabled");
            }

            var identity = CreateIdentity(userPrincipal);
            //var browserInfo = authenticationManager.CreateTwoFactorRememberBrowserIdentity(userPrincipal.Sid.ToString());
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //authenticationManager.SignOut(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            //authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity, browserInfo);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);


            return new AuthenticationResult();
        }


        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            //var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.WindowsAccountName, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.Sid.ToString()));
            if (!String.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }
            /*
                Public
                Clinical
                FrontDesk
                CDPHE
                SiteAdmin    
                Office 
                DACODS        
             */

            // get roles 


            var user = UserManager.AddSiteUser(userPrincipal.SamAccountName.ToLower(),
                                                   System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userPrincipal.DisplayName),
                                                    userPrincipal.EmailAddress);


            var rolesToAdd = new List<string>();
            rolesToAdd.Add("Public");

            var rolesToRemove = new List<string>();

            // CDPHE
            //using (var cdpheDB = new Areas.CDPHETracking.Models.CDPHETrackingContext())
            //{
            //    var nav = cdpheDB.Navigator.Where(x => x.UserName.Equals(user.UserName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            //    if (nav != null)
            //    {
            //        if (nav.IsAuthorized)
            //        {
            //            rolesToAdd.Add("CDPHE");
            //        }
            //        else
            //        {
            //            rolesToRemove.Add("CDPHE");
            //        }
            //    }
            //}

            //// Office (active login to Accumed)
            //if (USCS.Areas.ClientSearch.Helpers.DataHelper.IsUserAccumedEnabled(user.UserName))
            //    rolesToAdd.Add("Office");
            //else
            //    rolesToRemove.Add("Office");

            //// DACODS Enabled
            //if (USCS.Areas.ClientSearch.Helpers.DataHelper.IsDACODSEnabled(user.UserName))
            //    rolesToAdd.Add("DACODS");
            //else
            //    rolesToRemove.Add("DACODS");


            //// Clinical (active login to MindLinc)
            //if (USCS.Areas.ClientSearch.Helpers.DataHelper.IsUserMindLincEnabled(user.UserName))
            //    rolesToAdd.Add("Clinical");
            //else
            //    rolesToRemove.Add("Clinical");


            user = UserManager.RoleManager(user, rolesToAdd, rolesToRemove);

            foreach (var role in user.UserRoles)
            {
                identity.AddClaim(new Claim("role", role.Role.RoleName));
            }

            return identity;
        }
    }



    public static class UserManager
    {
        public static SiteUser AddSiteUser(string UserName, string FullName, string EmailAddress)
        {
            SiteUser ret;
            bool changed = false;
            using (var db = new UserManagerContext())
            {
                if (!(db.SiteUsers.ToList()).Exists(x => x.UserName.Equals(UserName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    ret = db.SiteUsers.Add(new SiteUser
                    {
                        UserName = UserName,
                        FullName = FullName,
                        EmailAddress = EmailAddress
                    });
                    changed = true;
                }
                else
                {

                    var user = db.SiteUsers.Where(x => x.UserName.Equals(UserName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                    if (user.FullName != FullName)
                    {
                        user.FullName = FullName;
                        changed = true;
                    }
                    if (user.EmailAddress != EmailAddress)
                    {
                        user.EmailAddress = EmailAddress;
                        changed = true;
                    }
                }

                if (changed)
                    db.SaveChanges();

                return db.SiteUsers.Where(x => x.UserName.Equals(UserName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            }
        }

        public static SiteUser RoleManager(SiteUser dbUser, List<string> RolesToAdd, List<string> RolesToRemove)
        {
            using (var db = new UserManagerContext())
            {
                List<SiteRole> siteRolesToAdd = new List<SiteRole>();
                List<SiteRole> siteRolesToRemove = new List<SiteRole>();
                List<string> distinctRoles = new List<string>();
                distinctRoles.AddRange(RolesToAdd.Distinct().ToList());
                distinctRoles.AddRange(RolesToRemove.Distinct().ToList());

                var modified = false;
                // make sure role exists
                foreach (var role in distinctRoles.Distinct())
                {
                    if (!(db.SiteRoles.ToList()).Exists(x => x.RoleName.Equals(role, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        db.SiteRoles.Add(new SiteRole { RoleName = role });
                        modified = true;
                    }
                }

                if (modified)
                {
                    db.SaveChanges();
                    modified = false;
                }

                var roles = db.SiteRoles;

                // get current user
                var user = db.SiteUsers.Find(dbUser.UserID);
                int cnt = user.UserRoles.Count();
                // remove what needs removed;
                user.UserRoles.RemoveAll(
                    ur => RolesToRemove.Any(rtr => ur.Role.RoleName.Equals(rtr, StringComparison.InvariantCultureIgnoreCase))
                    );

                if (cnt != user.UserRoles.Count())
                    modified = true;

                if (modified)
                {
                    db.SaveChanges();
                    modified = false;
                }

                // add what needs added
                foreach (var toAdd in RolesToAdd.Where(rta => !user.UserRoles.Any(u => rta.Equals(u.Role.RoleName, StringComparison.InvariantCultureIgnoreCase))).ToList().Distinct())
                {
                    var trole = db.SiteRoles.Where(x => x.RoleName.Equals(toAdd, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                    db.SiteUserRoles.Add(new SiteUserRole
                    {
                        RoleID = trole.RoleID,
                        UserID = dbUser.UserID
                    });

                    modified = true;

                }

                if (modified)
                {
                    db.SaveChanges();
                }

                return db.SiteUsers.Find(dbUser.UserID);
            }
        }

    }

    [Table("SiteUsers")]
    public class SiteUser
    {
        [Key]
        public int UserID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(25)]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }

        [NotMapped]
        private DateTime? createDate;

        public DateTime CreateDate { get { return this.createDate.HasValue ? this.createDate.Value : DateTime.Now; } set { createDate = value; } }
        public virtual List<SiteUserRole> UserRoles { get; set; }

    }

    [Table("SiteUserRoles")]
    public class SiteUserRole
    {
        [Key]
        public int SiteUserRoleID { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual SiteUser User { get; set; }
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual SiteRole Role { get; set; }

    }

    [Table("SiteRoles")]
    public class SiteRole
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }



    public class UserManagerContext : DbContext
    {
        public UserManagerContext() : base("name=USCS_Model")
        {


        }

        public DbSet<SiteUser> SiteUsers { get; set; }
        public DbSet<SiteRole> SiteRoles { get; set; }
        public DbSet<SiteUserRole> SiteUserRoles { get; set; }

    }

}