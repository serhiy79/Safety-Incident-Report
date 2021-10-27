namespace SIR
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SIRModel : DbContext
    {
        public SIRModel()
            : base("name=SIRModel")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<TActionPlan> TActionPlans { get; set; }
        public virtual DbSet<TEmployee> TEmployees { get; set; }
        public virtual DbSet<TIncidentReporting> TIncidentReportings { get; set; }
        public virtual DbSet<TRootCause> TRootCauses { get; set; }
        public virtual DbSet<TWhy> TWhies { get; set; }
        public virtual DbSet<TWorkLocation> TWorkLocations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<TActionPlan>()
                .Property(e => e.Owner)
                .IsUnicode(false);

            modelBuilder.Entity<TActionPlan>()
                .Property(e => e.DeadLine)
                .IsUnicode(false);

            modelBuilder.Entity<TActionPlan>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<TActionPlan>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.Shift)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .Property(e => e.ManagerName)
                .IsUnicode(false);

            modelBuilder.Entity<TEmployee>()
                .HasMany(e => e.TIncidentReportings)
                .WithRequired(e => e.TEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.Shift)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.IncidentLocation)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.Department)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.Equipment)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.InjuryType)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.IncidentType)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.Investigator)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.FirstAid)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .Property(e => e.ImageName)
                .IsUnicode(false);

            modelBuilder.Entity<TIncidentReporting>()
                .HasMany(e => e.TRootCauses)
                .WithRequired(e => e.TIncidentReporting)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRootCause>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TRootCause>()
                .Property(e => e.VerifiedNote)
                .IsUnicode(false);

            modelBuilder.Entity<TRootCause>()
                .HasMany(e => e.TActionPlans)
                .WithRequired(e => e.TRootCause)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRootCause>()
                .HasMany(e => e.TWhies)
                .WithRequired(e => e.TRootCause)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TWhy>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TWorkLocation>()
                .Property(e => e.LocationName)
                .IsUnicode(false);

            modelBuilder.Entity<TWorkLocation>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<TWorkLocation>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<TWorkLocation>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<TWorkLocation>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<TWorkLocation>()
                .HasMany(e => e.TEmployees)
                .WithRequired(e => e.TWorkLocation)
                .WillCascadeOnDelete(false);
        }
    }
}
