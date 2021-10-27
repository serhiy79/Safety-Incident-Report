namespace SIR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TActionPlan")]
    public partial class TActionPlan
    {
        [Key]
        public int ActionPlanID { get; set; }

        public int RootCauseID { get; set; }

        [Required]
        [StringLength(50)]
        public string Owner { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Date)]
        public string DeadLine { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual TRootCause TRootCause { get; set; }
    }
}
