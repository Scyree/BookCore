using Microsoft.AspNetCore.Http;

namespace ExploreBooks.Models.AuthorViewModels
{
    public class AuthorEditModel
    {
        public AuthorEditModel()
        {
        }
        
        public string Description { get; set; }

        public string Books { get; set; }

        public IFormFile Image { get; set; }

        public AuthorEditModel(string description)
        {
            Description = description;
        }
    }
}
