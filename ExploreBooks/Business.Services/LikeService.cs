﻿using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;

namespace Business.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _repository;

        public LikeService(ILikeRepository repository)
        {
            _repository = repository;
        }

        public List<Like> GetAllLikes()
        {
            return _repository.GetAllLikes();
        }

        public Like GetLikeById(Guid id)
        {
            return _repository.GetLikeById(id);
        }

        public void CreateLike(Like like)
        {
            _repository.CreateLike(like);
        }

        public void EditLike(Like like)
        {
            _repository.EditLike(like);
        }

        public void DeleteLike(Like like)
        {
            _repository.DeleteLike(like);
        }

        public int GetNumberOfLikes(Guid targetId)
        {
            var positiveLikes = GetAllLikes().Count(likes => likes.TargetId == targetId && likes.Positivity);
            var negativeLikes = GetAllLikes().Count(likes => likes.TargetId == targetId && !likes.Positivity);

            return positiveLikes - negativeLikes;
        }

        private void CheckIfExistsForUpvote(Guid targetId, Guid userId)
        {
            var check = GetAllLikes().Count(like => like.UserId == userId && like.TargetId == targetId);

            if (check == 0)
            {
                CreateLike(
                    Like.CreateLike(
                        userId,
                        targetId,
                        true
                    )
                );
            }
        }

        private void CheckIfExistsForDownvote(Guid targetId, Guid userId)
        {
            var check = GetAllLikes().Count(like => like.UserId == userId && like.TargetId == targetId);

            if (check == 0)
            {
                CreateLike(
                    Like.CreateLike(
                        userId,
                        targetId,
                        false
                    )
                );
            }
        }

        public void Upvote(Guid targetId, Guid userId)
        {
            var likeToBeEdited = _repository.GetLikeForUserAndTarget(userId, targetId);

            if (likeToBeEdited != null)
            {
                if (likeToBeEdited.Positivity)
                {
                    DeleteLike(likeToBeEdited);
                }
                else
                {
                    likeToBeEdited.Positivity = true;
                    EditLike(likeToBeEdited);
                }
            }
            else
            {
                CheckIfExistsForUpvote(targetId, userId);
            }
        }

        public void Downvote(Guid targetId, Guid userId)
        {
            var likeToBeEdited = _repository.GetLikeForUserAndTarget(userId, targetId);

            if (likeToBeEdited != null)
            {
                if (!likeToBeEdited.Positivity)
                {
                    DeleteLike(likeToBeEdited);
                }
                else
                {
                    likeToBeEdited.Positivity = false;
                    EditLike(likeToBeEdited);
                }
            }
            else
            {
                CheckIfExistsForDownvote(targetId, userId);
            }
        }

        public void DeleteUserLikes(Guid userId)
        {
            var likes = _repository.GetAllLikesForUserId(userId);

            foreach (var like in likes)
            {
                _repository.DeleteLike(like);
            }
        }
    }
}
