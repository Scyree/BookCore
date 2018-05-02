using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface ICommentService
    {
        IReadOnlyList<Comment> GetAllComments(Guid targetId);
        void CreateComment(Guid userId, Guid targetId, string text);
        void EditComment(Guid id, string text);
        void DeleteComment(Guid id);
        Comment GetCommentById(Guid id);
        void UpvoteComment(Guid commentId, Guid userId);
        void DownvoteComment(Guid commentId, Guid userId);
        Guid GetBookIdForATarget(Guid id);
    }
}
