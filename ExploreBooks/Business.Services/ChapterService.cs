using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;

namespace Business.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _repository;

        public ChapterService(IChapterRepository repository)
        {
            _repository = repository;
        }

        public bool CheckIfUserRecommendedChaptersForBook(Guid userId, Guid bookId)
        {
            return _repository.GetAllChapters().Any(chapter => chapter.UserId == userId && chapter.BookId == bookId);
        }

        public Chapter GetUserChaptersForBook(Guid userId, Guid bookId)
        {
            return _repository.GetAllChapters().SingleOrDefault(chapter => chapter.UserId == userId && chapter.BookId == bookId);
        }

        public IReadOnlyList<Chapter> GetAllChaptersForBook(Guid bookId)
        {
            return _repository.GetAllChapters().Where(chapter => chapter.BookId == bookId).ToList();
        }

        public string GetChaptersAverageForBook(Guid bookId)
        {
            var chapters = GetAllChaptersForBook(bookId);
            var numberOfChapters = new int[24];

            for (var index = 0; index < 24; ++index)
            {
                numberOfChapters[index] = 0;
            }

            if (chapters.Count > 0)
            {
                foreach (var chapter in chapters)
                {
                    var dividedChapters = chapter.Chapters.Split(",");

                    foreach (var dividedChapter in dividedChapters)
                    {
                        ++numberOfChapters[Int32.Parse(dividedChapter)];
                    }
                }
            }

            var recommendedChapters = "";

            for (var index = 0; index < 24; ++index)
            {
                if (numberOfChapters[index] >= 1)
                {
                    recommendedChapters += index + ", ";
                }
            }

            return recommendedChapters;
        }

        public void ChapterBook(Guid bookId, string userId, string chapters)
        {
            var check = CheckIfUserRecommendedChaptersForBook(Guid.Parse(userId), bookId);

            if (!check)
            {
                var chapter = Chapter.CreateChapter(Guid.Parse(userId), bookId);
                chapter.Chapters = chapters;

                _repository.CreateChapter(chapter);
            }
            else
            {
                var chapter = GetUserChaptersForBook(Guid.Parse(userId), bookId);
                chapter.Chapters = chapters;

                _repository.EditChapter(chapter);
            }
        }
    }
}
