using System.ComponentModel.DataAnnotations;

namespace PharmaManagementApp.Models
{
    public class MasterTable : BaseEntity
    {
        [Key]
        [Display(Name = "Pharmacy Id")]
        public int PharmacyId { get; set; }
        [Required(ErrorMessage = "Pharmacy Name is Required.")]
        [Display(Name = "Pharmacy Name")]
        public string PharmacyName { get; set; }
        [Required(ErrorMessage = "Email is Required.")]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "Database is Required.")]
        public string DbName { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
