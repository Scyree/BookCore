using System.Linq;
using System.Collections.Generic;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class PostMiddleware : IPostMiddleware
    {
        private readonly IPostRepository _repository;

        public PostMiddleware(IPostRepository repository)
        {
            _repository = repository;
        }
        
        public IReadOnlyList<Post> GetPostsByDate()
        {
            return _repository.GetAllPosts().OrderByDescending(post => post.Date).ToList();
        }

        public IReadOnlyList<Post> GetPostsBasedOnLikes()
        {
            return _repository.GetAllPosts().OrderByDescending(post => post.Likes).ToList();
        }
    }
}
