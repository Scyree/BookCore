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

        public List<Book> Books { get; set; }

        public List<Comment> Comments { get; set; }

        public static Author CreateAuthor(string name, string description, List<Book> books, List<Comment> comments)
        {
            var instance = new Author
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateAuthor(name, description, books, comments);

            return instance;
        }

        private void UpdateAuthor(string name, string description, List<Book> books, List<Comment> comments)
        {
            Name = name;
            Description = description;
            Books = books;
            Comments = comments;
        }
    }
}
