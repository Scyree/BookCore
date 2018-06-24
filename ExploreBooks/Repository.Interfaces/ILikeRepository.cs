using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface ILikeRepository
    {
        List<Like> GetAllLikes();
        List<Like> GetAllLikesForUserId(Guid userId);
        Like GetLikeForUserAndTarget(Guid userId, Guid targetId);
        Like GetLikeById(Guid id);
        void CreateLike(Like like);
        void EditLike(Like like);
        void DeleteLike(Like like);
    }
}
