using System.ComponentModel.DataAnnotations;

namespace PharmaApi.Models
{
    public class UserViewModel
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
