using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface ICommentMiddleware
    {
        List<Comment> GetAllCommentsGivenPostId(Guid postId);
        List<Comment> GetAllCommentsGivenPostIdSortedByDate(Guid postId);
        void DeleteUserComments(Guid userId);
    }
}
