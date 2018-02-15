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

        public List<Author> Authors { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Review> Reviews { get; set; }
        
        public List<Detail> Details { get; set; }

        public List<Recommandation> Recommandations { get; set; }

        public List<BuyingSite> BuyingSites { get; set; }

        public double FinalRate { get; set; }

        public List<Rating> Ratings { get; set; } 

        public static Book CreateBook(string title, string description, string folder, string imageName, List<Author> authors, List<Genre> genres, List<Review> reviews, List<Detail> details, List<Recommandation> recommandations, List<BuyingSite> buyingSites, List<Rating> ratings)
        {
            var instance = new Book
            {
                Id = Guid.NewGuid(),
                FinalRate = 0.0
            };

            instance.UpdateBook(title, description, folder, imageName, authors, genres, reviews, details, recommandations, buyingSites, ratings);

            return instance;
        }

        private void UpdateBook(string title, string description, string folder, string imageName, List<Author> authors, List<Genre> genres, List<Review> reviews, List<Detail> details, List<Recommandation> recommandations, List<BuyingSite> buyingSites, List<Rating> ratings)
        {
            Title = title;
            Description = description;
            Folder = folder;
            ImageName = imageName;
            Authors = authors;
            Genres = genres;
            Reviews = reviews;
            Details = details;
            Recommandations = recommandations;
            BuyingSites = buyingSites;
            Ratings = ratings;
        }
    }
}
