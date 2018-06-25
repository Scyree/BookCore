using System;
using System.Threading.Tasks;
using Business.Interfaces;
using ExploreBooks.Models.BookViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace ExploreBooks.Controllers
{
    [Route("books/[action]")]
    public class BooksController : Controller
    {
        private readonly IBookService _service;
        private readonly IGenreMiddleware _genreService;
        private readonly IAuthorService _authorService;
        private readonly IRecommendationService _recommendationService;

        public BooksController(IBookService service, IGenreMiddleware genreService, IAuthorService authorService, IRecommendationService recommendationService)
        {
            _service = service;
            _genreService = genreService;
            _authorService = authorService;
            _recommendationService = recommendationService;
        }
        
        public IActionResult Index(string pageNumber)
        {
            var value = 0;

            if (pageNumber != null)
            {
                value = Int32.Parse(pageNumber);
            }
            
            return View(_service.GetFirstNBooks(value));
        }

        [HttpGet, ActionName("search")]
        public IActionResult Search(string searchOption)
        {
            if (searchOption == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(_service.SearchBooks(searchOption));
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
        [Authorize(Roles = "Owner, Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("create")]
        [Authorize(Roles = "Owner, Administrator")]
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
        [Authorize(Roles = "Owner, Administrator")]
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
                book.Details,
                book.Id
            );

            return View(bookEditModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("edit")]
        [Authorize(Roles = "Owner, Administrator")]
        public async Task<IActionResult> Edit([Bind("BookId, Description, Details, Image")] BookEditModel bookEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookEditModel);
            }

            await _service.EditBook(bookEditModel.BookId, bookEditModel.Image, bookEditModel.Description, bookEditModel.Details);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("delete")]
        [Authorize(Roles = "Owner")]
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

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("delete")]
        [Authorize(Roles = "Owner")]
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
