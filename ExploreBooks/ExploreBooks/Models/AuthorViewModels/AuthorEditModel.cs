using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.AuthorViewModels
{
    public class AuthorEditModel
    {
        public AuthorEditModel()
        {
        }

       // [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }

     //   [Required(ErrorMessage = "A description is required.")]
     //   [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        public string Books { get; set; }

        public IFormFile Image { get; set; }

        public AuthorEditModel(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
