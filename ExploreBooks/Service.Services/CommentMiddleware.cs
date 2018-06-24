using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class CommentMiddleware : ICommentMiddleware
    {
        private readonly ICommentRepository _repository;

        public CommentMiddleware(ICommentRepository repository)
        {
            _repository = repository;
        }
        
        public List<Comment> GetAllCommentsGivenPostId(Guid postId)
        {
            return _repository.GetAllCommentsGivenPostId(postId);
        }

        public List<Comment> GetAllCommentsGivenPostIdSortedByDate(Guid postId)
        {
            return _repository.GetAllCommentsGivenPostIdSortedByDate(postId);
        }

        public void DeleteUserComments(Guid userId)
        {
            var comments = _repository.GetAllCommentsForUserId(userId);

            foreach (var comment in comments)
            {
                _repository.DeleteComment(comment);
            }
        }
    }
}
