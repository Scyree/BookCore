using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public DateTime JoinDate { get; set; }

        public string Folder { get; set; }

        public string ImageName { get; set; }

        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Recommendation> Recommandations { get; set; }

        public ICollection<BooksForMood> BooksForMoods { get; set; }

        public ICollection<FollowUser> Following { get; set; }

        public ICollection<BookState> Books { get; set; }
    }
}
