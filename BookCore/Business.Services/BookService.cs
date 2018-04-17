using Data.Domain.Interfaces.Repositories;
using Data.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly string _folder;

        public BookService(IBookRepository repository, IWorkingWithFiles fileManagement, IGenreService genreService, IAuthorService authorService, IAuthorBookRepository authorBookRepository)
        {
            _repository = repository;
            _fileManagement = fileManagement;
            _genreService = genreService;
            _authorService = authorService;
            _authorBookRepository = authorBookRepository;
            _folder = "Books";
        }
        
        public IReadOnlyList<Book> GetAllBooks()
        {
            var books = _repository.GetAllBooks();

            foreach (var book in books)
            {
                book.Authors = _authorBookRepository.GetAllAuthorBooksBasedOnBookId(book.Id);
                book.Genres = _genreService.GetGenreBasedOnBookId(book.Id);
            }

            return books;
        }

        public async Task CreateBook(IFormFile image, string title, string description, string details, string authors, string genres)
        {
            var genresList = _genreService.GetGenres(genres);
            var authorList = _authorService.GetAuthors(authors);
            var value = Guid.NewGuid();
            var folder = _fileManagement.GetPath(_folder, value);
            
            await  _fileManagement.CreateFile(_folder, value, image);
            
            var book = Book.CreateBook(
                title,
                description,
                folder,
                image.FileName,
                details,
                genresList
            );

            _repository.CreateBook(book);

            foreach (var genre in genresList)
            {
                genre.BooksId = book.Id;
            }

            foreach (var author in authorList)
            {
                var authorBook = AuthorBook.CreateAuthorBook(author.Id, book.Id);
                _authorBookRepository.CreateAuthorBook(authorBook);
            }
        }

        public async Task EditBook(Guid id, IFormFile image, string description, string details)
        {
            var bookToBeEdited = _repository.GetBookById(id);

            bookToBeEdited.Description = description;
            bookToBeEdited.Details = details;

            if (image != null)
            {
                var value = Guid.NewGuid();

                await _fileManagement.CreateFile(_folder, value, image);
            }

            _repository.EditBook(bookToBeEdited);
        }

        public void DeleteBook(Guid id)
        {
            _fileManagement.DeleteFolderForGivenId(_folder, id);
        }

        public Book GetBookById(Guid id)
        {
            return _repository.GetBookById(id);
        }

    }
}
