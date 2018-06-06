using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.BookViewModels
{
    public class BookEditModel
    {
        public BookEditModel()
        {
        }
        
        public string Authors { get; set; }
        
        public string Genres { get; set; }
        
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }
        
        public string Details { get; set; }

        public IFormFile Image { get; set; }

        public BookEditModel(string description, string details)
        {
            Description = description;
            Details = details;
        }
    }
}
