using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
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

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Recommandation> Recommandations { get; set; }

        public ICollection<BooksForMood> BooksForMoods { get; set; }

        public ICollection<ApplicationUser> Friends { get; set; }

        public ICollection<BookState> Books { get; set; }
    }
}
