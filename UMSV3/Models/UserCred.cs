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
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Invalid UserName")]
        public string UserName { get; set; }
        [Required]
        /* [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,12}$", ErrorMessage = "Please make sure you include: A Lowercase letter,Uppercase letter and at least One Number")]*/
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,13}$", ErrorMessage = "Password requires one lower case letter, one upper case letter, one digit, 6-12 length, and no spaces.")]
        public string Password { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
