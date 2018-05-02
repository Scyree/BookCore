using System;
using System.Collections.Generic;
using Business.Interfaces;
using Domain.Data;
using Service.Interfaces;

namespace Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentGeneralUsage _commentService;
        private readonly IReviewGeneralUsage _reviewService;
        private readonly ILikeService _likeService;

        public CommentService(ICommentGeneralUsage repository, IReviewGeneralUsage reviewService, ILikeService likeService)
        {
            _commentService = repository;
            _reviewService = reviewService;
            _likeService = likeService;
        }

        public IReadOnlyList<Comment> GetAllComments(Guid targetId)
        {
            return _commentService.GetAllCommentsGivenTargetId(targetId);
        }
        
        public void CreateComment(Guid userId, Guid targetId, string text)
        {
            var comment = Comment.CreateComment(
                userId,
                targetId,
                text
            );

            _commentService.CreateComment(comment);
        }

        public void EditComment(Guid id, string text)
        {
            var commentToBeEdited = _commentService.GetCommentById(id);

            if (commentToBeEdited != null)
            {
                if (text != null)
                {
                    commentToBeEdited.Text = text;
                }

                _commentService.EditComment(commentToBeEdited);
            }
        }

        public void DeleteComment(Guid id)
        {
            var commentToBeDeleted = _commentService.GetCommentById(id);

            if (commentToBeDeleted != null)
            {
                _commentService.DeleteComment(commentToBeDeleted);
            }
        }

        public Comment GetCommentById(Guid id)
        {
            return _commentService.GetCommentById(id);
        }

        public void UpvoteComment(Guid commentId, Guid userId)
        {
            _likeService.Upvote(commentId, userId);
        }

        public void DownvoteComment(Guid commentId, Guid userId)
        {
            _likeService.Downvote(commentId, userId);
        }

        public Guid GetBookIdForATarget(Guid id)
        {
            return _reviewService.GetReviewById(id).BookId;
        }
    }
}
