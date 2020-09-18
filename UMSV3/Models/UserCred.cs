namespace UMSV3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UserCred
    {
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9_]{2,29}$", ErrorMessage = "Please make sure to only use letters and numbers")]
        public string UserName { get; set; }
        [Required]
        /* [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,12}$", ErrorMessage = "Please make sure you include: A Lowercase letter,Uppercase letter and at least One Number")]*/
        public string Password { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
