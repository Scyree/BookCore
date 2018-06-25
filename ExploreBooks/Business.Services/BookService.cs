using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        private readonly IGenreMiddleware _genreService;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IPostService _postService;
        private readonly string _folder;

        public BookService(IBookRepository bookRepository, IWorkingWithFiles fileManagement, IGenreMiddleware genreService, IGenreBookMiddleware genreBookService, IAuthorMiddleware authorService, IAuthorBookMiddleware authorBookService, IPostService postService)
        {
            _bookRepository = bookRepository;
            _fileManagement = fileManagement;
            _genreService = genreService;
            _genreBookService = genreBookService;
            _authorService = authorService;
            _authorBookService = authorBookService;
            _postService = postService;
            _folder = "books";
        }

        public List<Book> SearchBooks(string text)
        {
            var books = _bookRepository.GetAllBooksContainingTheTitle(text);

            foreach (var book in books)
            {
                book.Authors = _authorBookService.GetAllAuthorBooksBasedOnBookId(book.Id);
                book.Genres = _genreBookService.GetAllGenreBooksBasedOnBookId(book.Id);
                book.Posts = _postService.GetAllPostsForTargetId(book.Id);
            }

            return books;
        }

        public List<Book> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            
            return books;
        }

        public List<Book> GetFirstNBooks(int count)
        {
            var books = _bookRepository.GetFirstNBooks(count, 12);
            
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

        public async Task EditBook(Guid id, IFormFile image, string description, string details)
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
                book.Authors = _authorBookService.GetAllAuthorBooksBasedOnBookId(id);
                book.Genres = _genreBookService.GetAllGenreBooksBasedOnBookId(id);
                book.Posts = _postService.GetAllPostsForTargetId(book.Id);

                return book;
            }

            return null;
        }

        public List<SelectListItem> GetAllBooksForRecommendation(Guid bookId)
        {
            var books = _bookRepository.GetAllBooksExcept(bookId);
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
