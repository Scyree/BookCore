using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public LikeRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public List<Like> GetAllLikes()
        {
            return _databaseService.Likes.ToList();
        }

        public List<Like> GetAllLikesForUserId(Guid userId)
        {
            return _databaseService.Likes.Where(user => user.UserId == userId).ToList();
        }

        public Like GetLikeForUserAndTarget(Guid userId, Guid targetId)
        {
            return _databaseService.Likes.FirstOrDefault(like => like.UserId == userId && like.TargetId == targetId);
        }

        public Like GetLikeById(Guid id)
        {
            return _databaseService.Likes.SingleOrDefault(like => like.Id == id);
        }

        public void CreateLike(Like like)
        {
            _databaseService.Likes.Add(like);

            _databaseService.SaveChanges();
        }

        public void EditLike(Like like)
        {
            _databaseService.Likes.Update(like);

            _databaseService.SaveChanges();
        }

        public void DeleteLike(Like like)
        {
            _databaseService.Likes.Remove(like);

            _databaseService.SaveChanges();
        }
    }
}
