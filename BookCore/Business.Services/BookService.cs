using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Middleware.Interfaces;

namespace Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookGeneralUsage _bookService;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IGenreService _genreService;
        private readonly IAuthorGeneralUsage _authorService;
        private readonly IAuthorBookService _authorBookService;
        private readonly string _folder;

        public BookService(IBookGeneralUsage bookService, IWorkingWithFiles fileManagement, IGenreService genreService, IAuthorGeneralUsage authorService, IAuthorBookService authorBookService)
        {
            _bookService = bookService;
            _fileManagement = fileManagement;
            _genreService = genreService;
            _authorService = authorService;
            _authorBookService = authorBookService;
            _folder = "Books";
        }
        
        public IReadOnlyList<Book> GetAllBooks()
        {
            var books = _bookService.GetAllBooks();

            foreach (var book in books)
            {
                book.Authors = _authorBookService.GetAllAuthorBooksBasedOnBookId(book.Id);
                book.Genres = _genreService.GetGenreBasedOnBookId(book.Id);
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

            if (image != null)
            {
                imageName = image.FileName;

                await _fileManagement.CreateFile(_folder, value, image);
            }
            else
            {
                _fileManagement.CopyFile(_folder, value);
            }
            
            var book = Book.CreateBook(
                title,
                description,
                path,
                imageName,
                details
            );

            _bookService.CreateBook(book);

            foreach (var genre in genresList)
            {
                genre.BooksId = book.Id;
            }

            foreach (var author in authorList)
            {
                _authorBookService.CheckAuthorBook(author.Id, book.Id);
            }
        }

        public async Task EditBook(Guid id, IFormFile image, string description, string details)
        {
            var bookToBeEdited = _bookService.GetBookById(id);

            bookToBeEdited.Description = description;
            bookToBeEdited.Details = details;

            if (image != null)
            {
                var value = Guid.NewGuid();

                await _fileManagement.CreateFile(_folder, value, image);
            }

            _bookService.EditBook(bookToBeEdited);
        }

        public void DeleteBook(Guid id)
        {
            _fileManagement.DeleteFolderForGivenId(_folder, id);
        }

        public Book GetBookById(Guid id)
        {
            return _bookService.GetBookById(id);
        }
    }
}
