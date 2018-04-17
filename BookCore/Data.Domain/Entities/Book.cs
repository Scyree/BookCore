using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Folder { get; set; }

        public string ImageName { get; set; }

        public ICollection<AuthorBook> Authors { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public ICollection<Review> Reviews { get; set; }
        
        public string Details { get; set; }

        public ICollection<Recommandation> Recommandations { get; set; }

        public ICollection<BuyingSite> BuyingSites { get; set; }

        public double FinalRate { get; set; }

        public List<Rating> Ratings { get; set; } 

        public static Book CreateBook(string title, string description, string folder, string imageName, string details, List<Genre> genres)
        {
            var instance = new Book
            {
                Id = Guid.NewGuid(),
                Authors = new List<AuthorBook>(),
                Reviews = new List<Review>(),
                Recommandations = new List<Recommandation>(),
                BuyingSites = new List<BuyingSite>(),
                Ratings = new List<Rating>(),
                FinalRate = 0.0
            };

            instance.UpdateBook(title, description, folder, imageName, details, genres);

            return instance;
        }

        private void UpdateBook(string title, string description, string folder, string imageName, string details, List<Genre> genres)
        {
            Title = title;
            Description = description;
            Folder = folder;
            ImageName = imageName;
            Genres = genres;
            Details = details;
        }
    }
}
