using System;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService _service;

        public LikeController(ILikeService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Upvote(Guid targetId, Guid userId)
        {
            _service.Upvote(targetId, userId);
        }

        [HttpPost]
        public void Downvote(Guid targetId, Guid userId)
        {
            _service.Downvote(targetId, userId);
        }

        [HttpGet]
        public int GetNumberOfLikes(Guid targetId)
        {
            return _service.GetNumberOfLikes(targetId);
        }
    }
}