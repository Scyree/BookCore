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
        Guid GetBookIdForATarget(Guid id);
    }
}
