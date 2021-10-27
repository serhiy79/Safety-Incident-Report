namespace SIR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TEmployee()
        {
            TIncidentReportings = new HashSet<TIncidentReporting>();
        }

        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers for the First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers for the MiddleName Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers for the LastName Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter only numbers for the phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers for the Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter only alphabets for the City")]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Enter only numbers for the zip")]
        public string Zip { get; set; }

        public int WorkLocationID { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM'/'dd'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Shift { get; set; }

        [Required]
        public int SafetyRecord { get; set; }

        [Required]
        public int IPMScore { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers for the Manager Name")]
        public string ManagerName { get; set; }

        public virtual TWorkLocation TWorkLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIncidentReporting> TIncidentReportings { get; set; }
    }
}
