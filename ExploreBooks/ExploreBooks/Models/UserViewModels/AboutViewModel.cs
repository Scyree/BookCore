
using System.Collections.Generic;
using Domain.Data;

namespace ExploreBooks.Models.UserViewModels
{
    public class AboutViewModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }
        
        public string Description { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
