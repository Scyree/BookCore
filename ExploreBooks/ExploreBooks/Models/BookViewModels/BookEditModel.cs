using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.BookViewModels
{
    public class BookEditModel
    {
        public BookEditModel()
        {
        }
        
        public Guid BookId { get; set; }

        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }
        
        public string Details { get; set; }

        public IFormFile Image { get; set; }

        public BookEditModel(string description, string details, Guid bookId)
        {
            Description = description;
            Details = details;
            BookId = bookId;
        }
    }
}
