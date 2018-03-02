using System;
using System.Collections.Generic;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Data.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        IReadOnlyList<Comment> GetAllComments();
        Comment GetCommentById(Guid id);
        void CreateComment(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);

        List<Comment> GetAllComments(Guid targetId);
    }
}
