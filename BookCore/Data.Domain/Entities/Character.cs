using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static Character CreateCharacter(string name, string description)
        {
            var instance = new Character { Id = Guid.NewGuid() };
            instance.UpdateCharacter(name, description);

            return instance;
        }

        private void UpdateCharacter(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
