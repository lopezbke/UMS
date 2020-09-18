using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UMSV3.Models
{
    public class EmailPasswordReset
    {   [Required]
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Invalid UserName")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("FirstName")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$",
         ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$",ErrorMessage = "Email not Valid")]
        public string Email { get; set; }
    }
}