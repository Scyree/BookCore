using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface ICommentMiddleware
    {
        IReadOnlyList<Comment> GetAllCommentsGivenPostId(Guid postId);
        IReadOnlyList<Comment> GetAllCommentsGivenPostIdSortedByDate(Guid postId);
    }
}
