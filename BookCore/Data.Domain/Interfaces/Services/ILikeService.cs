using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Data.Domain.Interfaces.Services
{
    public interface ILikeService
    {
        //From Repository
        IReadOnlyList<Like> GetAllLikes();
        Like GetLikeById(Guid id);
        void CreateLike(Like like);
        void EditLike(Like like);
        void DeleteLike(Like like);

        //New methods
        int GetNumberOfLikes(Guid reviewId);
        void Upvote(Guid targetId, Guid userId);
        void Downvote(Guid targetId, Guid userId);
    }
}
