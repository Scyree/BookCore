using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
