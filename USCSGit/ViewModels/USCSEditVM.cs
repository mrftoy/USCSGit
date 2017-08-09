using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using USCS.Models;
using USCS.ViewModels;

namespace USCS.ViewModels
{
    public class USCSEditVM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USCSEditVM()
        {
            Activities = new HashSet<Activity>();
            Categories = new HashSet<Category>();
            DocumentationRequirements = new HashSet<DocumentationRequirement>();
            Durations = new HashSet<Duration>();
            ModeOfDeliveries = new HashSet<ModeOfDelivery>();
            Modifiers = new HashSet<Modifier>();
            PlaceOfServices = new HashSet<PlaceOfService>();
            Populations = new HashSet<Population>();
            StaffRequirements = new HashSet<StaffRequirement>();
            Units = new HashSet<Unit>();
            Usages = new HashSet<Usage>();
            ProgramServiceCategories = new HashSet<ProgramServiceCategory>();
        }

        //public int ProcedureCodeID { get; set; }

        [StringLength(20)]
        public string CPT_HCPCS_Code { get; set; }

        [StringLength(1000)]
        public string ProcedureCodeDescription { get; set; }

        [StringLength(5000)]
        public string ServiceDescription { get; set; }

        [StringLength(5000)]
        public string ProcedureCodeNotes { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Eff_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Thru_Date { get; set; }

        [StringLength(10)]
        public string Active { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Load_Date { get; set; }

        //[StringLength(2000)]
        //public string Activity1 { get; set; }

        //[StringLength(250)]
        //public string Domain { get; set; }

        //[StringLength(250)]
        //public string PrimaryCategory { get; set; }

        //[StringLength(250)]
        //public string SecondaryCategory { get; set; }

        //[StringLength(250)]
        //public string TertiaryCategory { get; set; }

        //[StringLength(2000)]
        //public string DocumentationRequirement1 { get; set; }

        //[StringLength(20)]
        //public string MinimumValue { get; set; }

        //[StringLength(20)]
        //public string MaximumValue { get; set; }

        //[StringLength(50)]
        //public string DurationMetric { get; set; }

        //[StringLength(50)]
        //public string ModeOfDelivery1 { get; set; }

        //[StringLength(10)]
        //public string Modifier1 { get; set; }

        //[StringLength(250)]
        //public string ModifierType { get; set; }

        //[StringLength(250)]
        //public string ModifierDescription { get; set; }

        //[StringLength(2000)]
        //public string ModifierDefinition { get; set; }

        //[StringLength(20)]
        //public string POSCode { get; set; }

        //[StringLength(200)]
        //public string POSName { get; set; }

        //[StringLength(4000)]
        //public string POSDescription { get; set; }

        //[StringLength(20)]
        //public string Population1 { get; set; }

        //[StringLength(20)]
        //public string AgeRange { get; set; }

        //[StringLength(500)]
        //public string StaffLicensure { get; set; }

        //[StringLength(200)]
        //public string UnitMetric { get; set; }

        //[StringLength(20)]
        //public string Usage1 { get; set; }

        //[StringLength(50)]
        //public string ProgramServiceCategory1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentationRequirement> DocumentationRequirements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Duration> Durations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModeOfDelivery> ModeOfDeliveries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Modifier> Modifiers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaceOfService> PlaceOfServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Population> Populations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffRequirement> StaffRequirements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unit> Units { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usage> Usages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgramServiceCategory> ProgramServiceCategories { get; set; }

    }
}