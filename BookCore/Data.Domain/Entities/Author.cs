using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<AuthorBook> Books { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public static Author CreateAuthor(string name, string description)
        {
            var instance = new Author
            {
                Id = Guid.NewGuid(),
                Comments = new List<Comment>(),
                Books = new List<AuthorBook>()
            };

            instance.UpdateAuthor(name, description);

            return instance;
        }

        private void UpdateAuthor(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
