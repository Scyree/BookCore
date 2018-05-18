using System.Collections.Generic;
using Domain.Data;

namespace ExploreBooks.Models.UserViewModels
{
    public class LibraryViewModel
    {
        public ICollection<BookState> Books { get; set; }
    }
}
