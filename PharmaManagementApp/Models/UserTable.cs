using System.ComponentModel.DataAnnotations;

namespace PharmaManagementApp.Models
{
    public class UserTable : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name is Required.")]
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is Required.")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "Mobile Number is Required.")]
        [Display(Name = "Mobile Number")]
        [StringLength(13)]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Address is Required.")]
        [StringLength(500)]
        public string Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
