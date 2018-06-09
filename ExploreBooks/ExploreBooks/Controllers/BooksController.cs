using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using ExploreBooks.Models.BookViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    [Route("books/[action]")]
    public class BooksController : Controller
    {
        private readonly IBookService _service;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        private readonly IRecommendationService _recommendationService;

        public BooksController(IBookService service, IGenreService genreService, IAuthorService authorService, IRecommendationService recommendationService)
        {
            _service = service;
            _genreService = genreService;
            _authorService = authorService;
            _recommendationService = recommendationService;
        }
        
        public IActionResult Index()
        {
            return View(_service.GetAllBooks());
        }

        [HttpGet, ActionName("genres")]
        public IActionResult Genres(string genre)
        {
            if (genre == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(_genreService.GetBooksForSpecifiedGenre(genre));
        }

        [HttpGet, ActionName("authors")]
        public IActionResult Authors(string author)
        {
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(_authorService.GetBooksForSpecifiedAuthor(author));
        }

        [HttpGet, ActionName("recommendations")]
        public IActionResult Recommendations(Guid bookId)
        {
            var book = _service.GetBookById(bookId);

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(_recommendationService.GetAllRecommendationsForBookId(bookId));
        }

        [HttpGet, ActionName("details")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _service.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet, ActionName("create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost, ActionName("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Authors, Genres, Title, Description, Details, Image")] BookCreateModel bookCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookCreateModel);
            }

            await _service.CreateBook(bookCreateModel.Image, bookCreateModel.Title, bookCreateModel.Description, bookCreateModel.Details, bookCreateModel.Authors, bookCreateModel.Genres);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("edit")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _service.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }


            var bookEditModel = new BookEditModel(
                book.Description,
                book.Details
            );

            return View(bookEditModel);
        }
        
        [HttpPost, ActionName("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Authors, Genres, Description, Details, Image")] BookEditModel bookEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookEditModel);
            }

            await _service.EditBook(id, bookEditModel.Image, bookEditModel.Description, bookEditModel.Details, bookEditModel.Genres, bookEditModel.Authors);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("delete")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _service.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        
        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteBook(id);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public void RecommendABook(Guid bookId, Guid userId, string recommendedBookId, string reason)
        {
            _recommendationService.MakeARecommendation(bookId, userId, Guid.Parse(recommendedBookId), reason);
        }
    }
}
