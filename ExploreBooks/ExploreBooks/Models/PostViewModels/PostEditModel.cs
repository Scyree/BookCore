using System.ComponentModel.DataAnnotations;

namespace ExploreBooks.Models.PostViewModels
{
    public class PostEditModel
    {
        public PostEditModel()
        {
        }
        
        //[Required(ErrorMessage = "A description is required.")]
       // [StringLength(2000, ErrorMessage = "Maximum number of characters is 2000!")]
        public string Description { get; set; }

        public PostEditModel(string description)
        {
            Description = description;
        }
    }
}
