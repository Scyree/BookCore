using System;
using System.Collections.Generic;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentMiddleware _commentService;
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentMiddleware repository, ICommentRepository commentRepository)
        {
            _commentService = repository;
            _commentRepository = commentRepository;
        }

        public IReadOnlyList<Comment> GetAllComentsForThePostGivenCommentId(Guid commentId)
        {
            var post = _commentRepository.GetCommentById(commentId);

            if (post != null)
            {
                return _commentService.GetAllCommentsGivenPostIdSortedByDate(post.PostId);
            }

            return null;
        }

        public IReadOnlyList<Comment> GetAllComments(Guid postId)
        {
            return _commentService.GetAllCommentsGivenPostIdSortedByDate(postId);
        }
        
        public void CreateComment(Guid userId, Guid postId, string text)
        {
            var comment = Comment.CreateComment(
                userId,
                postId,
                text
            );

            _commentRepository.CreateComment(comment);
        }

        public void EditComment(Guid id, string text)
        {
            var commentToBeEdited = _commentRepository.GetCommentById(id);

            if (commentToBeEdited != null)
            {
                if (text != null)
                {
                    commentToBeEdited.Text = text;
                }

                _commentRepository.EditComment(commentToBeEdited);
            }
        }

        public void DeleteComment(Guid id)
        {
            var commentToBeDeleted = _commentRepository.GetCommentById(id);

            if (commentToBeDeleted != null)
            {
                _commentRepository.DeleteComment(commentToBeDeleted);
            }
        }

        public Comment GetCommentById(Guid id)
        {
            return _commentRepository.GetCommentById(id);
        }
    }
}
