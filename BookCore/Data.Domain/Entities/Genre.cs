using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        public ICollection<GenreBook> Books { get; set; }

        public string Text { get; set; }

        public static Genre CreateGenre(string text)
        {
            var instance = new Genre
            {
                Id = Guid.NewGuid(),
                Books = new List<GenreBook>()
            };

            instance.UpdateGenre(text);

            return instance;
        }

        private void UpdateGenre(string text)
        {
            Text = text;
        }
    }
}
