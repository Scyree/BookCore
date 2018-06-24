using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
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

        public List<Comment> GetAllCommentsGivenPostId(Guid postId)
        {
            return _databaseService.Comments.Where(comments => comments.PostId == postId).ToList();
        }

        public List<Comment> GetAllCommentsGivenPostIdSortedByDate(Guid postId)
        {
            return _databaseService.Comments.Where(comments => comments.PostId == postId).OrderBy(comment => comment.Date).ToList();
        }

        public List<Comment> GetAllComments()
        {
            return _databaseService.Comments.ToList();
        }

        public List<Comment> GetAllCommentsForUserId(Guid userId)
        {
            return _databaseService.Comments.Where(user => user.UserId == userId).ToList();
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
    }
}
