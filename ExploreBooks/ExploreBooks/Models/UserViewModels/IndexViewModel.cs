using System.Collections.Generic;
using Domain.Data;

namespace ExploreBooks.Models.UserViewModels
{
    public class IndexViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }
        
        public string Description { get; set; }

        public string Username { get; set; }
        
        public IEnumerable<BookState> BookActivity { get; set; }

        public string StatusMessage { get; set; }
    }
}
