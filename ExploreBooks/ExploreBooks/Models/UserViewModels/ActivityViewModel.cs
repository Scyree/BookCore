using Domain.Data;
using System.Collections.Generic;

namespace ExploreBooks.Models.UserViewModels
{
    public class ActivityViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }
        
        public IEnumerable<BookState> BookActivity { get; set; }
    }
}
