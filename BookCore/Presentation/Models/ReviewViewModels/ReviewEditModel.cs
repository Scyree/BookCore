using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ReviewViewModels
{
    public class ReviewEditModel
    {
        public ReviewEditModel()
        {
        }

        [Required(ErrorMessage = "Please rate this book.")]
        public double BookRating { get; set; }

        [Required(ErrorMessage = "An user is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "A book is required.")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        public ReviewEditModel(Guid userId, Guid bookId, string description, double bookRating)
        {
            UserId = userId;
            BookId = bookId;
            Description = description;
            BookRating = bookRating;
        }
    }
}
