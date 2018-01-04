using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        public static Book CreateBook(string title, string description)
        {
            var instance = new Book { Id = Guid.NewGuid() };
            instance.UpdateBook(title, description);

            return instance;
        }

        private void UpdateBook(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
