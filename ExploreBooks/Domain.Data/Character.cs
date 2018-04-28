using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static Character CreateCharacter(Guid bookId, string name, string description)
        {
            var instance = new Character
            {
                Id = Guid.NewGuid()
            };
            instance.UpdateCharacter(bookId, name, description);

            return instance;
        }

        private void UpdateCharacter(Guid bookId, string name, string description)
        {
            BookId = bookId;
            Name = name;
            Description = description;
        }
    }
}
