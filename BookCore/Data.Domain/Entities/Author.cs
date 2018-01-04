using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        public static Author CreateAuthor(string name, string description)
        {
            var instance = new Author { Id = Guid.NewGuid() };
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
