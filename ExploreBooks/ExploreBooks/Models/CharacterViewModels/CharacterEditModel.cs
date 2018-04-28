using System;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.CharacterViewModels
{
    public class CharacterEditModel
    {
        public CharacterEditModel()
        {
        }

        [Required(ErrorMessage = "BookId Id is required.")]
        public Guid BookId { get; set; }


        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        public CharacterEditModel(Guid bookId, string name, string description)
        {
            BookId = bookId;
            Name = name;
            Description = description;
        }
    }
}
