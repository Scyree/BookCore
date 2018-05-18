using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;

namespace Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookGeneralUsage _bookService;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IGenreService _genreService;
        private readonly IGenreBookService _genreBookService;
        private readonly IAuthorGeneralUsage _authorService;
        private readonly IAuthorBookService _authorBookService;
        private readonly IReviewService _reviewService;
        private readonly string _folder;

        public BookService(IBookGeneralUsage bookService, IWorkingWithFiles fileManagement, IGenreService genreService, IGenreBookService genreBookService, IAuthorGeneralUsage authorService, IAuthorBookService authorBookService, IReviewService reviewService)
        {
            _bookService = bookService;
            _fileManagement = fileManagement;
            _genreService = genreService;
            _genreBookService = genreBookService;
            _authorService = authorService;
            _authorBookService = authorBookService;
            _reviewService = reviewService;
            _folder = "books";
        }
        
        public IReadOnlyList<Book> GetAllBooks()
        {
            var books = _bookService.GetAllBooks();

            foreach (var book in books)
            {
                book.Authors = _authorBookService.GetAllAuthorBooksBasedOnBookId(book.Id);
                book.Genres = _genreBookService.GetAllGenreBooksBasedOnBookId(book.Id);
                book.Reviews = _reviewService.GetAllReviews().Where(review => review.BookId == book.Id).ToList();
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
            var finalDescription = "Momentan nu exista o descriere a cartii";
            var finalDetails = "Momentan nu exista detalii suplimentare ale cartii";

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

            _bookService.CreateBook(book);

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
            var bookToBeEdited = _bookService.GetBookById(id);

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

                _bookService.EditBook(bookToBeEdited);
            }
        }

        public void DeleteBook(Guid id)
        {
            var bookToBeDeleted = _bookService.GetBookById(id);

            if (bookToBeDeleted != null)
            {
                _bookService.DeleteBook(bookToBeDeleted);
                _authorBookService.DeleteForBookId(id);
                _genreBookService.DeleteForBookId(id);
            }
        }

        public Book GetBookById(Guid id)
        {
            var book = _bookService.GetBookById(id);

            if (book != null)
            {
                book.Authors = _authorBookService.GetAllAuthorBooksBasedOnBookId(id);
                book.Genres = _genreBookService.GetAllGenreBooksBasedOnBookId(id);
                book.Reviews = _reviewService.GetAllReviews().Where(review => review.BookId == id).ToList();

                return book;
            }

            return null;
        }
    }
}
