using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface ILikeService
    {
        //From Repository
        List<Like> GetAllLikes();
        Like GetLikeById(Guid id);
        void CreateLike(Like like);
        void EditLike(Like like);
        void DeleteLike(Like like);

        //New methods
        int GetNumberOfLikes(Guid targetId);
        void Upvote(Guid targetId, Guid userId);
        void Downvote(Guid targetId, Guid userId);
        void DeleteUserLikes(Guid userId);
    }
}
