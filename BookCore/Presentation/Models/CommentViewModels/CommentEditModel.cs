using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.CommentViewModels
{
    public class CommentEditModel
    {
        public CommentEditModel()
        {
        }
        
        public string Text { get; set; }

        public CommentEditModel(string text)
        {
            Text = text;
        }
    }
}
