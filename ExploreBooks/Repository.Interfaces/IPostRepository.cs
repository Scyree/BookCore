using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();
        List<Post> GetAllPostsForUserId(Guid userId);
        List<Post> GetAllPostsForTargetId(Guid targetId);
        Post GetPostById(Guid id);
        void CreatePost(Post post);
        void EditPost(Post post);
        void DeletePost(Post post);
    }
}
