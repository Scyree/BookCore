using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.BookViewModels
{
    public class BookEditModel
    {
        public BookEditModel()
        {
        }

        //[Required(ErrorMessage = "At least one author is required.")]
        public string Authors { get; set; }

        //[Required(ErrorMessage = "At least one genre is required.")]
        public string Genres { get; set; }

       // [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "A description is required.")]
       // [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Details { get; set; }

        public IFormFile Image { get; set; }

        public BookEditModel(string description, string details)
        {
            Description = description;
            Details = details;
        }
    }
}
