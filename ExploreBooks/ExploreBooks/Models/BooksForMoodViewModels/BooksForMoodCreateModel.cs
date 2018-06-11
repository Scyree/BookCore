﻿using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.BooksForMoodViewModels
{
    public class BooksForMoodCreateModel
    {
        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "At least one book is required.")]
        public string Books { get; set; }
    }
}