using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IChapterService
    {
        bool CheckIfUserRecommendedChaptersForBook(Guid userId, Guid bookId);
        Chapter GetUserChaptersForBook(Guid userId, Guid bookId);
        IReadOnlyList<Chapter> GetAllChaptersForBook(Guid bookId);
        string GetChaptersAverageForBook(Guid bookId);
        void ChapterBook(Guid bookId, string userId, string chapters);
    }
}
