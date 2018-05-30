using System.Collections.Generic;
using Domain.Data;

namespace ExploreBooks.Models.UserViewModels
{
    public class LibraryViewModel
    {
        public string Username { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
