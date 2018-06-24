using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface ICommentService
    {
        List<Comment> GetAllComentsForThePostGivenCommentId(Guid commentId);
        List<Comment> GetAllComments(Guid postId);
        void CreateComment(Guid userId, Guid postId, string text);
        void EditComment(Guid id, string text);
        void DeleteComment(Guid id);
        Comment GetCommentById(Guid id);
    }
}
