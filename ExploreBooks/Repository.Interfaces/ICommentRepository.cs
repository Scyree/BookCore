using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task<IReadOnlyList<Comment>> GetAllComments();
        Task<Comment> GetCommentById(Guid id);
        Task CreateComment(Comment comment);
        Task EditComment(Comment comment);
        Task DeleteComment(Comment comment);

        Task<IReadOnlyList<Comment>> GetAllComments(Guid targetId);
    }
}
