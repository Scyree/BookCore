using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Middleware.Interfaces;

namespace Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IBookGeneralUsage _bookService;
        private readonly IAuthorGeneralUsage _authorService;
        private readonly IAuthorBookService _authorBookService;
        private readonly string _folder;

        public AuthorService(IAuthorGeneralUsage authorService, IWorkingWithFiles fileManagement, IBookGeneralUsage bookService, IAuthorBookService authorBookService)
        {
            _authorService = authorService;
            _fileManagement = fileManagement;
            _bookService = bookService;
            _authorBookService = authorBookService;
            _folder = "Authors";
        }

        public IReadOnlyList<Author> GetAllAuthors()
        {
            var authors = _authorService.GetAllAuthors();

            foreach (var author in authors)
            {
                author.Books = _authorBookService.GetAllAuthorBooksBasedOnAuthorId(author.Id);
            }

            return authors;
        }
        
        public async Task CreateAuthor(IFormFile image, string name, string description, string books)
        {
            var bookList = _bookService.GetBooks(books);
            var value = Guid.NewGuid();
            var path = _folder + "\\" + value;
            var imageName = _folder + ".jpg";

            await _fileManagement.CreateFile(_folder, value, image);

            if (image != null)
            {
                imageName = image.FileName;

                await _fileManagement.CreateFile(_folder, value, image);
            }
            else
            {
                _fileManagement.CopyFile(_folder, value);
            }

            var author = Author.CreateAuthor(
                description,
                "Momentan nu exista o descriere a acestui autor",
                path,
                imageName
            );

            _authorService.CreateAuthor(author);

            foreach (var book in bookList)
            {
                _authorBookService.CheckAuthorBook(author.Id, book.Id);
            }
        }

        public async Task EditAuthor(Guid id, IFormFile image, string name, string description)
        {
            var authorToBeEdited = _authorService.GetAuthorById(id);

            authorToBeEdited.Name = name;
            authorToBeEdited.Description = description;

            if (image != null)
            {
                var value = Guid.NewGuid();

                await _fileManagement.CreateFile(_folder, value, image);
            }

            _authorService.EditAuthor(authorToBeEdited);
        }

        public void DeleteAuthor(Guid id)
        {
            _fileManagement.DeleteFolderForGivenId(_folder, id);
        }

        public Author GetAuthorById(Guid id)
        {
            return _authorService.GetAuthorById(id);
        }
    }
}
