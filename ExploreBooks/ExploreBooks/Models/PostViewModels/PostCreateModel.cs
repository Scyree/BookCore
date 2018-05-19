using System;
using System.ComponentModel.DataAnnotations;


namespace ExploreBooks.Models.PostViewModels
{
    public class PostCreateModel
    {
        [Required(ErrorMessage = "An user is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "A book is required.")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }
    }
}
