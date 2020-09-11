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

    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            this.ProfilePictures = new HashSet<ProfilePicture>();
        }

        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9_]{2,29}$", ErrorMessage = "Please make sure to only use letters and numbers")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters and numbers are not allowed.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters and numbers are not allowed.")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DisplayName("Address")]
        public string C_Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [DisplayName("Status")]
        public Nullable<int> StatusId { get; set; }
        [DisplayName("Role")]
        public Nullable<int> RoleId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfilePicture> ProfilePictures { get; set; }
        public virtual Role Role { get; set; }
        public virtual Status Status { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
