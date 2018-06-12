using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public ChapterRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Chapter> GetAllChapters()
        {
            return _databaseService.Chapters.ToList();
        }

        public Chapter GetChapterById(Guid id)
        {
            return _databaseService.Chapters.SingleOrDefault(chapter => chapter.Id == id);
        }

        public void CreateChapter(Chapter chapter)
        {
            _databaseService.Chapters.Add(chapter);

            _databaseService.SaveChanges();
        }

        public void EditChapter(Chapter chapter)
        {
            _databaseService.Chapters.Update(chapter);

            _databaseService.SaveChanges();
        }

        public void DeleteChapter(Chapter chapter)
        {
            _databaseService.Chapters.Remove(chapter);

            _databaseService.SaveChanges();
        }
    }
}
