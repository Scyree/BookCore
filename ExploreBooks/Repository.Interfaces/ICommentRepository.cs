using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface ICommentRepository
    {
        List<Comment> GetAllCommentsGivenPostId(Guid postId);
        List<Comment> GetAllCommentsGivenPostIdSortedByDate(Guid postId);
        List<Comment> GetAllComments();
        List<Comment> GetAllCommentsForUserId(Guid userId);
        Comment GetCommentById(Guid id);
        void CreateComment(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
