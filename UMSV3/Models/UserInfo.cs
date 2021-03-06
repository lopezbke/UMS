//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UMSV3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            this.ProfilePictures = new HashSet<ProfilePicture>();
             
        }

        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Invalid UserName")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$",
         ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$",
         ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Email not Valid")]
        public string Email { get; set; }
        [DisplayName("Address")]
        [Required]
        public string C_Address { get; set; }
       [Required]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$",
         ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$",
         ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string Country { get; set; }
       [Required]
       [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Use standard 5 digit US Zip Codes, or the US ZIP + 4 Standard.")]
        public string ZipCode { get; set; }
        [Required]
        [RegularExpression(@"^[2-9]\d{2}-\d{3}-\d{4}$", ErrorMessage = "Please use the following format 787-666-1234")]
        public string PhoneNumber { get; set; }
        [DisplayName("Status")]
        public Nullable<int> StatusId { get; set; }
        [DisplayName("Role")]
        public Nullable<int> RoleId { get; set; }

        public HttpPostedFileBase fileUpload { get; set; }

        public byte[] imageBuffer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfilePicture> ProfilePictures { get; set; }
        public virtual Role Role { get; set; }
        public virtual Status Status { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
