using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public DateTime JoinDate { get; set; }

        public string Folder { get; set; }

        public string ImageName { get; set; }

        public string Description { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Recommandation> Recommandations { get; set; }

        public List<BooksForMood> BooksForMoods { get; set; }

        public List<ApplicationUser> Friends { get; set; }

        public List<Favorite> Favorites { get; set; }
    }
}
