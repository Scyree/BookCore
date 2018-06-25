using System;
using Microsoft.AspNetCore.Http;

namespace ExploreBooks.Models.AuthorViewModels
{
    public class AuthorEditModel
    {
        public AuthorEditModel()
        {
        }
        
        public Guid AuthorId { get; set; }

        public string Description { get; set; }

        public string Books { get; set; }

        public IFormFile Image { get; set; }

        public AuthorEditModel(string description, Guid authorId)
        {
            Description = description;
            AuthorId = authorId;
        }
    }
}
