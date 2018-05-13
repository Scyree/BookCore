using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
