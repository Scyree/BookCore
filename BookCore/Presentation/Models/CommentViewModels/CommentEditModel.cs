using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.CommentViewModels
{
    public class CommentEditModel
    {
        public CommentEditModel()
        {
        }

        [Required(ErrorMessage = "Inser a target id")]
        public Guid TargetId { get; set; }

        [Required(ErrorMessage = "An user is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "A text is required.")]
        public string Text { get; set; }

        public CommentEditModel(Guid targetId, Guid userId, string text)
        {
            TargetId = targetId;
            UserId = userId;
            Text = text;
        }
    }
}
