using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Persistence;

namespace Business.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public CommentRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Comment> GetAllComments()
        {
            return _databaseService.Comments.ToList();
        }

        public Comment GetCommentById(Guid id)
        {
            return _databaseService.Comments.SingleOrDefault(comment => comment.Id == id);
        }

        public void CreateComment(Comment comment)
        {
            _databaseService.Comments.Add(comment);

            _databaseService.SaveChanges();
        }

        public void EditComment(Comment comment)
        {
            _databaseService.Comments.Update(comment);

            _databaseService.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            _databaseService.Comments.Remove(comment);

            _databaseService.SaveChanges();
        }

        public List<Comment> GetAllComments(Guid targetId)
        {
            return _databaseService.Comments.Where(comments => comments.TargetId == targetId).ToList();
        }
    }
}
