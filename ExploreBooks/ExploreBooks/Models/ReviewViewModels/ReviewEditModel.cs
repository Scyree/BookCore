using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.ReviewViewModels
{
    public class ReviewEditModel
    {
        public ReviewEditModel()
        {
        }

       // [Required(ErrorMessage = "Please rate this book.")]
        public double BookRating { get; set; }

        //[Required(ErrorMessage = "A description is required.")]
       // [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        public ReviewEditModel(string description, double bookRating)
        {
            Description = description;
            BookRating = bookRating;
        }
    }
}
