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
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DisplayName("Confirm New Password")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Temporary or Old Password")]
        public string OldPassword { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}