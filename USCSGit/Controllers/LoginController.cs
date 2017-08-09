//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace USCS.Controllers
//{
//    public class LoginController : Controller
//    {
//        // GET: Login
//        public ActionResult Index()
//        {
//            return View();
//        }

//        // GET: Login/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: Login/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Login/Create
//        [HttpPost]
//        public ActionResult Create(FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Login/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: Login/Edit/5
//        [HttpPost]
//        public ActionResult Edit(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add update logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Login/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: Login/Delete/5
//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}

using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using USCS.Models;
using Microsoft.Owin.Security;
using static USCS.Startup;
using Microsoft.AspNet.Identity;

namespace USCS.Controllers
{
    public class LoginController : Controller
    {

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginViewModel model, string returnURL)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ViewBag.ReturnUrl = returnURL;
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            var authenticationResult = authService.SignIn(model.Username, model.Password);

            if (authenticationResult.IsSuccess)
            {
                // we are in!
                return RedirectToLocal(returnURL);
            }

            ModelState.AddModelError("", authenticationResult.ErrorMessage);

            return RedirectToLocal("");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Menu", "Home");
        }

        [ValidateAntiForgeryToken]
        public virtual ActionResult Logoff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToLocal("");
        }
    }
}