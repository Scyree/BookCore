using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Data.Domain.Interfaces.Services
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
    }
}
