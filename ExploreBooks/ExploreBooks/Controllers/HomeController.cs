using System.Diagnostics;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExploreBooks.Models;

namespace ExploreBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUtilityService _utilityService;

        public HomeController(IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNews(string content)
        {
            TempData["Announcement"] = content;
            _utilityService.AddNews(content);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
