using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface ILikeRepository
    {
        Task<IReadOnlyList<Like>> GetAllLikes();
        Task<Like> GetLikeById(Guid id);
        Task CreateLike(Like like);
        Task EditLike(Like like);
        Task DeleteLike(Like like);
    }
}
