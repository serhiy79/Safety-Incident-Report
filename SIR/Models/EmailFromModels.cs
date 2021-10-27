using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SIR.Models
{
    public class EmailFromModels
    {
        [Required, Display(Name = "IncidentID")]
        public int IncidentID { get; set; }

        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }
       
        public string Message { get; set; }
    }
}