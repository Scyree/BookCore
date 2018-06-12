using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IChapterRepository
    {
        IReadOnlyList<Chapter> GetAllChapters();
        Chapter GetChapterById(Guid id);
        void CreateChapter(Chapter chapter);
        void EditChapter(Chapter chapter);
        void DeleteChapter(Chapter chapter);
    }
}
