using System;
using System.Collections.Generic;
using Domain.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business.Interfaces
{
    public interface IApplicationBookLogic
    {
        bool CheckIfBookStateExists(Guid bookId, string userId);
        void ReadActions(Guid bookId, string userId, string actionName);
        List<Book> GetBooksOfAUser(string userId);
        void AddToFavorites(Guid bookId, string userId);
        void RemoveFromFavorites(Guid bookId, string userId);
        void ModifyBookPages(Guid bookId, string userId, string number);
        string GetNumberOfPages(Guid bookId, string userId);
        int GetAllBooksNumber(string userId);
        int GetPlanToReadBooksNumber(string userId);
        int GetReadingBooksNumber(string userId);
        int GetReadBooksNumber(string userId);
        List<Book> GetAllBooksBasedOnState(string userId, int givenState);
        List<Book> GetFavoriteBooks(string userId);
        bool CheckIfUserRatedThisBook(Guid userId, Guid bookId);
        double GetUserRatingForBook(Guid userId, Guid bookId);
        double GetRatingsAverageForBook(Guid bookId);
        List<SelectListItem> GetRatingList();
        string ConvertRateToText(double rate);
        void RateBook(Guid bookId, string userId, double value);
        bool CheckIfUserRecommendedChaptersForBook(Guid userId, Guid bookId);
        string GetUserChaptersForBook(Guid userId, Guid bookId);
        string GetChaptersAverageForBook(Guid bookId);
        void ChapterBook(Guid bookId, string userId, string chapters);
    }
}
