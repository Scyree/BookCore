using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface ILikeRepository
    {
        IReadOnlyList<Like> GetAllLikes();
        Like GetLikeById(Guid id);
        void CreateLike(Like like);
        void EditLike(Like like);
        void DeleteLike(Like like);
    }
}
