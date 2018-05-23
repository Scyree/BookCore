using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        public string User { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Password between {2} and {1} chars", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
