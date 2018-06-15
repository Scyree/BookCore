using System.Diagnostics;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExploreBooks.Models;

namespace ExploreBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenreService _genreService;

        public HomeController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
