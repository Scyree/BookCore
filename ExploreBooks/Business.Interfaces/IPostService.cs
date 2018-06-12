using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IPostService
    {
        IReadOnlyList<Post> GetOnlyFirstNumberOfPosts(int number);
        IReadOnlyList<Post> GetAllPostsForTargetId(Guid targetId);
        IReadOnlyList<Post> GetAllPosts(); 
        void CreatePost(Guid userId, Guid targetId, string description);
        void EditPost(Guid id, string description);
        void DeletePost(Guid id);
        Post GetPostById(Guid postId);
        
    }
}
