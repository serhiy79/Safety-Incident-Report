namespace SIR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TIncidentReporting")]
    public partial class TIncidentReporting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIncidentReporting()
        {
            TRootCauses = new HashSet<TRootCause>();
        }

        [Key]
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Enter only  numbers for the IncidentID")]
        public int IncidentID { get; set; }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Shift { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(50)]
        public string ExpirienceMonth { get; set; }

        [Required]
        [StringLength(50)]
        public string IncidentLocation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM'/'dd'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        //[RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
        public TimeSpan Time { get; set; }

        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z .&'/-]+)", ErrorMessage = "Enter only alphabets for the Equipment")]
        public string Equipment { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z .&'/-]+)", ErrorMessage = "Enter only alphabets for the InjuryType")]
        public string InjuryType { get; set; }

        [Required]
        [StringLength(50)]
        public string IncidentType { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z .&'/-]+)", ErrorMessage = "Enter only alphabets for the Investigator")]
        public string Investigator { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z .&'/-]+)", ErrorMessage = "Enter only alphabets for the FirstAid")]
        public string FirstAid { get; set; }

        [StringLength(200)]
        public string ImageName { get; set; }

        public byte[] ImageData { get; set; }

        public virtual TEmployee TEmployee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRootCause> TRootCauses { get; set; }
    }
}
