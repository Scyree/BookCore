using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IPostMiddleware
    {
        List<Post> GetPostsByDate();
        List<Post> GetPostsBasedOnLikes();
        void DeleteUserPosts(Guid userId);
    }
}
