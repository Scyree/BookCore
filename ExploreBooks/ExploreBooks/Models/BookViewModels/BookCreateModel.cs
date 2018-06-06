using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.BookViewModels
{
    public class BookCreateModel
    {
        [Required(ErrorMessage = "At least one author is required.")]
        public string Authors { get; set; }

        [Required(ErrorMessage = "At least one genre is required.")]
        public string Genres { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Details { get; set; }

        public IFormFile Image { get; set; }
    }
}
