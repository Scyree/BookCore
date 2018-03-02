using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly ILikeService _likeService;

        public CommentService(ICommentRepository repository, ILikeService likeService)
        {
            _repository = repository;
            _likeService = likeService;
        }

        public IReadOnlyList<Comment> GetAllComments()
        {
            return _repository.GetAllComments();
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

        public List<Comment> GetAllComments(Guid targetId)
        {
            return _repository.GetAllComments(targetId);
        }
    }
}
