using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IPostRepository
    {
        IReadOnlyList<Post> GetAllPosts();
        Post GetPostById(Guid id);
        void CreatePost(Post post);
        void EditPost(Post post);
        void DeletePost(Post post);
    }
}
