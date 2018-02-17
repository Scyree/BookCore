using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.BookViewModels
{
    public class BookEditModel
    {
        public BookEditModel()
        {
        }

        [Required(ErrorMessage = "An author is required.")]
        public Guid AuthorId { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Details { get; set; }

        public IFormFile Image { get; set; }

        public BookEditModel(Guid authorId, string title, string description, string details)
        {
            AuthorId = authorId;
            Title = title;
            Description = description;
            Details = details;
        }
    }
}
