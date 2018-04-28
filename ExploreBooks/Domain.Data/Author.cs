using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Folder { get; set; }

        public string ImageName { get; set; }

        public ICollection<AuthorBook> Books { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public static Author CreateAuthor(string name, string description, string folder, string imageName)
        {
            var instance = new Author
            {
                Id = Guid.NewGuid(),
                Comments = new List<Comment>(),
                Books = new List<AuthorBook>()
            };

            instance.UpdateAuthor(name, description, folder, imageName);

            return instance;
        }

        private void UpdateAuthor(string name, string description, string folder, string imageName)
        {
            Name = name;
            Description = description;
            Folder = folder;
            ImageName = imageName;
        }
    }
}
