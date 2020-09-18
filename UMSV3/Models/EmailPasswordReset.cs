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
        public string UserName { get; set; }
        [Required]
        [DisplayName("FirstName")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}