using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace UMSV3.Models
{
    public class PasswordReset
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Invalid UserName")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,13}$", ErrorMessage = "Password requires one lower case letter, one upper case letter, one digit, 6-12 length, and no spaces.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DisplayName("Confirm New Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,13}$", ErrorMessage = "Password requires one lower case letter, one upper case letter, one digit, 6-12 length, and no spaces.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DisplayName("Temporary or Old Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,13}$", ErrorMessage = "Password requires one lower case letter, one upper case letter, one digit, 6-12 length, and no spaces.")]
        public string OldPassword { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}