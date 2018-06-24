using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public string Folder { get; set; }

        public string ImageName { get; set; }

        public string Details { get; set; }

        public double FinalRate { get; set; }

        public ICollection<AuthorBook> Authors { get; set; }

        public ICollection<GenreBook> Genres { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Recommendation> Recommandations { get; set; }

        public static Book CreateBook(string title, string description, string folder, string imageName, string details)
        {
            var random = new Random();
            var instance = new Book
            {
                Id = Guid.NewGuid(),
                Authors = new List<AuthorBook>(),
                Genres = new List<GenreBook>(),
                Posts = new List<Post>(),
                Pages = random.Next(250) + 1,
                Recommandations = new List<Recommendation>(),
                FinalRate = 0.0
            };

            instance.UpdateBook(title, description, folder, imageName, details);

            return instance;
        }

        private void UpdateBook(string title, string description, string folder, string imageName, string details)
        {
            Title = title;
            Description = description;
            Folder = folder;
            ImageName = imageName;
            Details = details;
        }
    }
}
