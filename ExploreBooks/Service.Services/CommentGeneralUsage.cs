using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class CommentGeneralUsage : ICommentGeneralUsage
    {
        private readonly ICommentRepository _repository;

        public CommentGeneralUsage(ICommentRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyList<Comment> GetAllComments()
        {
            return _repository.GetAllComments();
        }

        public IReadOnlyList<Comment> GetAllCommentsGivenTargetId(Guid targetId)
        {
            return _repository.GetAllComments().Where(comment => comment.TargetId == targetId).OrderByDescending(comments => comments.Date).ToList();
        }

        public Comment GetCommentById(Guid id)
        {
            return _repository.GetCommentById(id);
        }

        public void CreateComment(Comment comment)
        {
            _repository.CreateComment(comment);
        }

        public void EditComment(Comment comment)
        {
            _repository.EditComment(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _repository.DeleteComment(comment);
        }
    }
}
