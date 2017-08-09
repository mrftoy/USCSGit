using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using USCS.Models;

namespace USCS.ViewModels
{
    public class USCSFormVM
    {
        //public static implicit operator USCSFormVM(ProcedureCode procedurecode)
        //{
        //    return new USCSFormVM
        //    {
        //        CPT_HCPCS_Code = procedurecode.CPT_HCPCS_Code,
        //        ProcedureCodeDescription = procedurecode.ProcedureCodeDescription,
        //        ServiceDescription = procedurecode.ServiceDescription,
        //        ProcedureCodeNotes = procedurecode.ProcedureCodeNotes,
        //        Eff_Date = procedurecode.Eff_Date,
        //        Thru_Date = procedurecode.Thru_Date,
        //        Active = procedurecode.Active,
        //        Load_Date = procedurecode.Load_Date
        //    };
        //}

        //public static implicit operator ProcedureCode(USCSFormVM vm)
        //{
        //    return new ProcedureCode
        //    {
        //        CPT_HCPCS_Code = vm.CPT_HCPCS_Code,
        //        ProcedureCodeDescription = vm.ProcedureCodeDescription,
        //        ServiceDescription = vm.ServiceDescription,
        //        ProcedureCodeNotes = vm.ProcedureCodeNotes,
        //        Eff_Date = vm.Eff_Date,
        //        Thru_Date = vm.Thru_Date,
        //        Active = vm.Active,
        //        Load_Date = vm.Load_Date
        //    };
        //}
        [StringLength(10)]
        public string Active { get; set; }

        [StringLength(10)]
        public string Deleted { get; set; }

        [Required]
        [StringLength(250)]
        public string Domain { get; set; }

        [Required]
        [StringLength(20)]
        public string CPT_HCPCS_Code { get; set; }

        //[Required]
        //[StringLength(10)]
        //public string Modifier1 { get; set; }

        [Required]
        [StringLength(250)]
        public string PrimaryCategory { get; set; }

        [Required]
        [StringLength(250)]
        public string SecondaryCategory { get; set; }

        [Required]
        [StringLength(250)]
        public string TertiaryCategory { get; set; }

        [StringLength(1000)]
        public string ProcedureCodeDescription { get; set; }

        [StringLength(5000)]
        public string ServiceDescription { get; set; }

        [StringLength(2000)]
        public string DocumentationRequirement1 { get; set; }

        [StringLength(5000)]
        public string ProcedureCodeNotes { get; set; }

        [StringLength(2000)]
        public string Activity1 { get; set; }

        [StringLength(20)]
        public string MinimumValue { get; set; }

        [StringLength(20)]
        public string MaximumValue { get; set; }

        [StringLength(50)]
        public string DurationMetric { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Eff_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Thru_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Load_Date { get; set; }
    }
}