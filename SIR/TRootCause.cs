namespace SIR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRootCause")]
    public partial class TRootCause
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRootCause()
        {
            TActionPlans = new HashSet<TActionPlan>();
            TWhies = new HashSet<TWhy>();
        }

        [Key]
        public int RootCauseID { get; set; }

        public int IncidentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public bool Verified { get; set; }

        [Required]
        [StringLength(200)]
        public string VerifiedNote { get; set; }

        [Required]
        [StringLength(50)]
        public string RootCauseName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TActionPlan> TActionPlans { get; set; }

        public virtual TIncidentReporting TIncidentReporting { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TWhy> TWhies { get; set; }
    }
}
