using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public CommentRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IReadOnlyList<Comment>> GetAllComments()
        {
            return await _databaseService.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentById(Guid id)
        {
            return await _databaseService.Comments.SingleOrDefaultAsync(comment => comment.Id == id);
        }

        public async Task CreateComment(Comment comment)
        {
            _databaseService.Comments.Add(comment);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditComment(Comment comment)
        {
            _databaseService.Comments.Update(comment);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteComment(Comment comment)
        {
            _databaseService.Comments.Remove(comment);

            await _databaseService.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Comment>> GetAllComments(Guid targetId)
        {
            return await _databaseService.Comments.Where(comments => comments.TargetId == targetId).ToListAsync();
        }
    }
}
