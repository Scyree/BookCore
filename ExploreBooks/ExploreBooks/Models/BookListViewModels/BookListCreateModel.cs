using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.BookListViewModels
{
    public class BookListCreateModel
    {
        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "At least one book is required.")]
        public List<string> Books { get; set; }
    }
}
