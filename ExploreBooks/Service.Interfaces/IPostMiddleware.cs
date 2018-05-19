using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IPostMiddleware
    {
        IReadOnlyList<Post> GetPostsByDate();
        IReadOnlyList<Post> GetPostsBasedOnLikes();
    }
}
