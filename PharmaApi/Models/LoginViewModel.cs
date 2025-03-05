using System.ComponentModel.DataAnnotations;

namespace PharmaApi.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is Required.")]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
