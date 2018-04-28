using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface ICommentGeneralUsage
    {
        IReadOnlyList<Comment> GetAllComments();
        IReadOnlyList<Comment> GetAllCommentsGivenTargetId(Guid targetId);
        Comment GetCommentById(Guid id);
        void CreateComment(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
