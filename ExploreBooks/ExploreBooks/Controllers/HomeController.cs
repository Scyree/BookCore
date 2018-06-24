using System.Diagnostics;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExploreBooks.Models;
using Service.Interfaces;

namespace ExploreBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUtilityService _utilityService;
        private readonly IWorkingWithDatabase _service;

        public HomeController(IUtilityService utilityService, IWorkingWithDatabase service)
        {
            _utilityService = utilityService;
            _service = service;
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
            //_service.PopulateTextFiles();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return RedirectToAction("Index");
        }
    }
}
