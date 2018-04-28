using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface ICommentRepository
    {
        IReadOnlyList<Comment> GetAllComments();
        Comment GetCommentById(Guid id);
        void CreateComment(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);

        List<Comment> GetAllComments(Guid targetId);
    }
}
