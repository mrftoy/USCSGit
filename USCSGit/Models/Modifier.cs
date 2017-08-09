namespace USCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Modifier")]
    public partial class Modifier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modifier()
        {
            ProcedureCodes = new HashSet<ProcedureCode>();
        }

        public int ModifierID { get; set; }

        [Column("Modifier")]
        [StringLength(10)]
        public string Modifier1 { get; set; }

        [StringLength(250)]
        public string ModifierType { get; set; }

        [StringLength(250)]
        public string ModifierDescription { get; set; }

        [StringLength(2000)]
        public string ModifierDefinition { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Eff_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Thru_Date { get; set; }

        [Required]
        [StringLength(10)]
        public string Active { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Load_Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcedureCode> ProcedureCodes { get; set; }
    }
}
