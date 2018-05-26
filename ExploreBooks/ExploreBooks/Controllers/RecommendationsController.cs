using System;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    public class RecommendationsController : Controller
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public IActionResult GetAllRecommendations(Guid bookId)
        {
            return View(_recommendationService.GetAllRecommendationsForBookId(bookId));
        }
    }
}