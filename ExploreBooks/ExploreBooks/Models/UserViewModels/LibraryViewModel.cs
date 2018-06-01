using System.Collections.Generic;
using Domain.Data;

namespace ExploreBooks.Models.UserViewModels
{
    public class LibraryViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
