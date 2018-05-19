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
        
        public IReadOnlyList<Comment> GetAllCommentsGivenPostId(Guid postId)
        {
            return _repository.GetAllComments().Where(comments => comments.PostId == postId).ToList();
        }

        public IReadOnlyList<Comment> GetAllCommentsGivenPostIdSortedByDate(Guid postId)
        {
            return _repository.GetAllComments().Where(comments => comments.PostId == postId).OrderBy(comment => comment.Date).ToList();
        }
    }
}
