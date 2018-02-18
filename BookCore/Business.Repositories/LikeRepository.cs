using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Persistence;

namespace Business.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public LikeRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Like> GetAllLikes()
        {
            return _databaseService.Likes.ToList();
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
