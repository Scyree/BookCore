using System;
using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.CommentViewModels
{
    public class CommentCreateModel
    {
        [Required(ErrorMessage = "Inser a target id")]
        public Guid TargetId { get; set; }

        [Required(ErrorMessage = "An user is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "A text is required.")]
        public string Text { get; set; }
    }
}
