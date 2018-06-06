using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.AuthorViewModels
{
    public class AuthorCreateModel
    {
        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Books { get; set; }

        public IFormFile Image { get; set; }
    }
}
