namespace USCS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class USCSModel : DbContext
    {
        public USCSModel()
            : base("name=USCS_Model")
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<DocumentationRequirement> DocumentationRequirements { get; set; }
        public virtual DbSet<Duration> Durations { get; set; }
        public virtual DbSet<ICD> ICDs { get; set; }
        public virtual DbSet<ModeOfDelivery> ModeOfDeliveries { get; set; }
        public virtual DbSet<Modifier> Modifiers { get; set; }
        public virtual DbSet<ModifierType> ModifierTypes { get; set; }
        public virtual DbSet<PlaceOfService> PlaceOfServices { get; set; }
        public virtual DbSet<Population> Populations { get; set; }
        public virtual DbSet<ProcedureCode> ProcedureCodes { get; set; }
        public virtual DbSet<ProgramServiceCategory> ProgramServiceCategories { get; set; }
        public virtual DbSet<StaffRequirement> StaffRequirements { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Usage> Usages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .Property(e => e.Activity1)
                .IsUnicode(false);

            modelBuilder.Entity<Activity>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.ProcedureCodes)
                .WithMany(e => e.Activities)
                .Map(m => m.ToTable("ProcedureCodeActivity").MapLeftKey("ActivityID").MapRightKey("ProcedureCodeID"));

            modelBuilder.Entity<Category>()
                .Property(e => e.Deleted)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.ProcedureCodes)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("ProcedureCodeCategory").MapLeftKey("CategoryID").MapRightKey("ProcedureCodeID"));

            modelBuilder.Entity<DocumentationRequirement>()
                .Property(e => e.DocumentationRequirement1)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentationRequirement>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentationRequirement>()
                .HasMany(e => e.ProcedureCodes)
                .WithMany(e => e.DocumentationRequirements)
                .Map(m => m.ToTable("ProcedureCodeDocumentationRequirement").MapLeftKey("DocumentationRequirementID").MapRightKey("ProcedureCodeID"));

            modelBuilder.Entity<Duration>()
                .Property(e => e.MinimumValue)
                .IsUnicode(false);

            modelBuilder.Entity<Duration>()
                .Property(e => e.MaximumValue)
                .IsUnicode(false);

            modelBuilder.Entity<Duration>()
                .Property(e => e.DurationMetric)
                .IsUnicode(false);

            modelBuilder.Entity<Duration>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<Duration>()
                .HasMany(e => e.ProcedureCodes)
                .WithMany(e => e.Durations)
                .Map(m => m.ToTable("ProcedureCodeDuration").MapLeftKey("DurationID").MapRightKey("ProcedureCodeID"));

            modelBuilder.Entity<ICD>()
                .Property(e => e.ICDVersion)
                .IsUnicode(false);

            modelBuilder.Entity<ICD>()
                .Property(e => e.ICDCode)
                .IsUnicode(false);

            modelBuilder.Entity<ICD>()
                .Property(e => e.ICDDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ICD>()
                .Property(e => e.Deleted)
                .IsUnicode(false);

            modelBuilder.Entity<ModeOfDelivery>()
                .Property(e => e.ModeOfDelivery1)
                .IsUnicode(false);

            modelBuilder.Entity<ModeOfDelivery>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<ModeOfDelivery>()
                .HasMany(e => e.ProcedureCodes)
                .WithMany(e => e.ModeOfDeliveries)
                .Map(m => m.ToTable("ProcedureCodeModeOfDelivery").MapLeftKey("ModeOfDeliveryID").MapRightKey("ProcedureCodeID"));

            modelBuilder.Entity<Modifier>()
                .Property(e => e.Modifier1)
                .IsUnicode(false);

            modelBuilder.Entity<Modifier>()
                .Property(e => e.ModifierType)
                .IsUnicode(false);

            modelBuilder.Entity<Modifier>()
                .Property(e => e.ModifierDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Modifier>()
                .Property(e => e.ModifierDefinition)
                .IsUnicode(false);

            modelBuilder.Entity<Modifier>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<Modifier>()
                .HasMany(e => e.ProcedureCodes)
                .WithMany(e => e.Modifiers)
                .Map(m => m.ToTable("ProcedureCodeModifier").MapLeftKey("ModifierID").MapRightKey("ProcedureCodeID"));

            modelBuilder.Entity<ModifierType>()
                .Property(e => e.ModifierType1)
                .IsUnicode(false);

            modelBuilder.Entity<ModifierType>()
                .Property(e => e.Deleted)
                .IsUnicode(false);

            modelBuilder.Entity<PlaceOfService>()
                .Property(e => e.POSCode)
                .IsUnicode(false);

            modelBuilder.Entity<PlaceOfService>()
                .Property(e => e.POSName)
                .IsUnicode(false);

            modelBuilder.Entity<PlaceOfService>()
                .Property(e => e.POSDescription)
                .IsUnicode(false);

            modelBuilder.Entity<PlaceOfService>()
                .Property(e => e.Deleted)
                .IsUnicode(false);

            modelBuilder.Entity<PlaceOfService>()
                .HasMany(e => e.ProcedureCodes)
                .WithMany(e => e.PlaceOfServices)
                .Map(m => m.ToTable("ProcedureCodePlaceOfService").MapLeftKey("PlaceOfServiceID").MapRightKey("ProcedureCodeID"));

            modelBuilder.Entity<Population>()
                .Property(e => e.Population1)
                .IsUnicode(false);

            modelBuilder.Entity<Population>()
                .Property(e => e.AgeRange)
                .IsUnicode(false);

            modelBuilder.Entity<Population>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<Population>()
                .HasMany(e => e.ProcedureCodes)
                .WithMany(e => e.Populations)
                .Map(m => m.ToTable("ProcedureCodePopulation").MapLeftKey("PopulationID").MapRightKey("ProcedureCodeID"));

            modelBuilder.Entity<ProcedureCode>()
                .Property(e => e.CPT_HCPCS_Code)
                .IsUnicode(false);

            modelBuilder.Entity<ProcedureCode>()
                .Property(e => e.ProcedureCodeDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ProcedureCode>()
                .Property(e => e.ServiceDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ProcedureCode>()
                .Property(e => e.ProcedureCodeNotes)
                .IsUnicode(false);

            modelBuilder.Entity<ProcedureCode>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<ProcedureCode>()
                .HasMany(e => e.ProgramServiceCategories)
                .WithMany(e => e.ProcedureCodes)
                .Map(m => m.ToTable("ProcedureCodeProgramServiceCategory").MapLeftKey("ProcedureCodeID").MapRightKey("ProgramServiceCategoryID"));

            modelBuilder.Entity<ProcedureCode>()
                .HasMany(e => e.StaffRequirements)
                .WithMany(e => e.ProcedureCodes)
                .Map(m => m.ToTable("ProcedureCodeStaffRequirement").MapLeftKey("ProcedureCodeID").MapRightKey("StaffRequirementID"));

            modelBuilder.Entity<ProcedureCode>()
                .HasMany(e => e.Units)
                .WithMany(e => e.ProcedureCodes)
                .Map(m => m.ToTable("ProcedureCodeUnit").MapLeftKey("ProcedureCodeID").MapRightKey("UnitID"));

            modelBuilder.Entity<ProcedureCode>()
                .HasMany(e => e.Usages)
                .WithMany(e => e.ProcedureCodes)
                .Map(m => m.ToTable("ProcedureCodeUsage").MapLeftKey("ProcedureCodeID").MapRightKey("UsageID"));

            modelBuilder.Entity<ProgramServiceCategory>()
                .Property(e => e.ProgramServiceCategory1)
                .IsUnicode(false);

            modelBuilder.Entity<ProgramServiceCategory>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<StaffRequirement>()
                .Property(e => e.StaffLicensure)
                .IsUnicode(false);

            modelBuilder.Entity<StaffRequirement>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.UnitMetric)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<Usage>()
                .Property(e => e.Usage1)
                .IsUnicode(false);
        }
    }
}
