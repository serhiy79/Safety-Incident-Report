namespace SIR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TWhy")]
    public partial class TWhy
    {
        [Key]
        public int WhyID { get; set; }

        public int RootCauseID { get; set; }

        public int Ordering { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual TRootCause TRootCause { get; set; }
    }
}
