using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(string id)
        {
            return View("Index");
        }

        [HttpGet("FutureMessage")]
        public IActionResult UserNotFound()
        {
            return View();
        }

        [HttpGet("SomethingWentWrong")]
        public IActionResult SomethingWentWrong()
        {
            return View();
        }

        [HttpGet("ObjectNotFound")]
        public IActionResult ObjectNotFound()
        {
            return View();
        }
    }
}