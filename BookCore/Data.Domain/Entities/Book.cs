using System;
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

        public static Book CreateBook(string title, string description, string folder, string imageName)
        {
            var instance = new Book { Id = Guid.NewGuid() };
            instance.UpdateBook(title, description, folder, imageName);

            return instance;
        }

        private void UpdateBook(string title, string description, string folder, string imageName)
        {
            Title = title;
            Description = description;
            Folder = folder;
            ImageName = imageName;
        }
    }
}
