using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorMiddleware _authorService;
        private readonly IAuthorBookMiddleware _authorBookService;
        private readonly IGenreBookMiddleware _genreBookService;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IGenreService _genreService;
        private readonly IPostService _postService;
        private readonly IRatingService _ratingService;
        private readonly string _folder;

        public BookService(IBookRepository bookRepository, IWorkingWithFiles fileManagement, IGenreService genreService, IGenreBookMiddleware genreBookService, IAuthorMiddleware authorService, IAuthorBookMiddleware authorBookService, IPostService postService, IRatingService ratingService)
        {
            _bookRepository = bookRepository;
            _fileManagement = fileManagement;
            _genreService = genreService;
            _genreBookService = genreBookService;
            _authorService = authorService;
            _authorBookService = authorBookService;
            _postService = postService;
            _ratingService = ratingService;
            _folder = "books";
        }
        
        public IReadOnlyList<Book> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();

            foreach (var book in books)
            {
                book.Authors = _authorBookService.GetAllAuthorBooksBasedOnBookId(book.Id).ToList();
                book.Genres = _genreBookService.GetAllGenreBooksBasedOnBookId(book.Id).ToList();
                book.Posts = _postService.GetAllPosts().Where(post => post.TargetId == book.Id).ToList();
                book.Ratings = _ratingService.GetAllRatingsForBook(book.Id).ToList();
            }

            return books;
        }
        
        public async Task CreateBook(IFormFile image, string title, string description, string details, string authors, string genres)
        {
            var genresList = _genreService.GetGenres(genres);
            var authorList = _authorService.GetAuthors(authors);
            var value = Guid.NewGuid();
            var path = _folder + "\\" + value;
            var imageName = _folder + ".jpg";
            var finalDescription = "No description for this book at the moment..";
            var finalDetails = "There are mysteries surrounding this book..";

            if (description != null)
            {
                finalDescription = description;
            }

            if (details != null)
            {
                finalDetails = details;
            }

            if (image != null)
            {
                imageName = image.FileName;

                await _fileManagement.CreateFile(path, image);
            }
            else
            {
                _fileManagement.CopyFile(_folder, value);
            }
            
            var book = Book.CreateBook(
                title,
                finalDescription,
                path,
                imageName,
                finalDetails
            );

            _bookRepository.CreateBook(book);

            foreach (var genre in genresList)
            {
                _genreBookService.CheckGenreBook(genre.Id, book.Id);
            }

            foreach (var author in authorList)
            {
                _authorBookService.CheckAuthorBook(author.Id, book.Id);
            }
        }

        public async Task EditBook(Guid id, IFormFile image, string description, string details, string genres, string authors)
        {
            var bookToBeEdited = _bookRepository.GetBookById(id);

            if (bookToBeEdited != null)
            {
                if (description != null)
                {
                    bookToBeEdited.Description = description;
                }

                if (details != null)
                {
                    bookToBeEdited.Details = details;
                }

                if (genres != null)
                {
                    var genresList = _genreService.GetGenres(genres);

                    foreach (var genre in genresList)
                    {
                        _genreBookService.CheckGenreBook(genre.Id, id);
                    }
                }

                if (authors != null)
                {
                    var authorList = _authorService.GetAuthors(authors);

                    foreach (var author in authorList)
                    {
                        _authorBookService.CheckAuthorBook(author.Id, id);
                    }
                }

                if (image != null)
                {
                    var path = bookToBeEdited.Folder;
                    bookToBeEdited.ImageName = image.FileName;

                    await _fileManagement.CreateFile(path, image);
                }

                _bookRepository.EditBook(bookToBeEdited);
            }
        }

        public void DeleteBook(Guid id)
        {
            var bookToBeDeleted = _bookRepository.GetBookById(id);

            if (bookToBeDeleted != null)
            {
                _fileManagement.DeleteFolder(bookToBeDeleted.Folder);
                _bookRepository.DeleteBook(bookToBeDeleted);
                _authorBookService.DeleteForBookId(id);
                _genreBookService.DeleteForBookId(id);
            }
        }

        public Book GetBookById(Guid id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book != null)
            {
                book.Authors = _authorBookService.GetAllAuthorBooksBasedOnBookId(id).ToList();
                book.Genres = _genreBookService.GetAllGenreBooksBasedOnBookId(id).ToList();
                book.Posts = _postService.GetAllPosts().Where(post => post.TargetId == id).ToList();
                book.Ratings = _ratingService.GetAllRatingsForBook(book.Id).ToList();

                return book;
            }

            return null;
        }

        public List<SelectListItem> GetAllBooksForRecommendation(Guid bookId)
        {
            var books = _bookRepository.GetAllBooks().Where(book => book.Id != bookId);
            var bookList = new List<SelectListItem>();

            foreach (var book in books)
            {
                bookList.Add(new SelectListItem
                {
                    Text = book.Title,
                    Value = book.Id.ToString()
                });
            }

            return bookList;
        }
    }
}
