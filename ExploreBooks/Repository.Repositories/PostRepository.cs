using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public PostRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Post> GetAllPosts()
        {
            return _databaseService.Posts.ToList();
        }

        public Post GetPostById(Guid id)
        {
            return _databaseService.Posts.SingleOrDefault(post => post.Id == id);
        }

        public void CreatePost(Post post)
        {
            _databaseService.Posts.Add(post);

            _databaseService.SaveChanges();
        }

        public void EditPost(Post post)
        {
            _databaseService.Posts.Update(post);

            _databaseService.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _databaseService.Posts.Remove(post);

            _databaseService.SaveChanges();
        }
    }
}
