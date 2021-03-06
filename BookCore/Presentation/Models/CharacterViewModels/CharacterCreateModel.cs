﻿using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.CharacterViewModels
{
    public class CharacterCreateModel
    {
        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }
        
    }
}
