using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IReadOnlyList<Like>> GetAllLikes()
        {
            return await _databaseService.Likes.ToListAsync();
        }

        public async Task<Like> GetLikeById(Guid id)
        {
            return await _databaseService.Likes.SingleOrDefaultAsync(like => like.Id == id);
        }

        public async Task CreateLike(Like like)
        {
            _databaseService.Likes.Add(like);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditLike(Like like)
        {
            _databaseService.Likes.Update(like);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteLike(Like like)
        {
            _databaseService.Likes.Remove(like);

            await _databaseService.SaveChangesAsync();
        }
    }
}
