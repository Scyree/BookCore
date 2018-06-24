using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IPostService
    {
        List<Post> GetOnlyFirstNumberOfPosts(int number);
        List<Post> GetAllPostsForTargetId(Guid targetId);
        List<Post> GetAllPosts(); 
        void CreatePost(Guid userId, Guid targetId, string description);
        void EditPost(Guid id, string description);
        void DeletePost(Guid id);
        Post GetPostById(Guid postId);
        
    }
}
