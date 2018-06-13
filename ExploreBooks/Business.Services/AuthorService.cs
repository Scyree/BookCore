using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Microsoft.AspNetCore.Http;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IBookMiddleware _bookService;
        private readonly IAuthorBookMiddleware _authorBookService;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPostService _postService;
        private readonly string _folder;

        public AuthorService(IWorkingWithFiles fileManagement, IBookMiddleware bookService, IAuthorBookMiddleware authorBookService, IAuthorRepository authorRepository, IPostService postService)
        {
            _fileManagement = fileManagement;
            _bookService = bookService;
            _authorBookService = authorBookService;
            _authorRepository = authorRepository;
            _postService = postService;
            _folder = "authors";
        }

        public IReadOnlyList<Author> GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();

            foreach (var author in authors)
            {
                author.Books = _authorBookService.GetAllAuthorBooksBasedOnAuthorId(author.Id).ToList();
            }

            return authors;
        }

        public IReadOnlyList<Author> GetFirstNAuthors(int count)
        {
            var authors = _authorRepository.GetAllAuthors().OrderByDescending(author => author.Name).Skip(count).Take(2).ToList();

            foreach (var author in authors)
            {
                author.Books = _authorBookService.GetAllAuthorBooksBasedOnAuthorId(author.Id).ToList();
            }

            return authors;
        }

        public IReadOnlyList<Book> GetBooksForSpecifiedAuthor(string givenAuthor)
        {
            var author = _authorRepository.GetAllAuthors().SingleOrDefault(auth => ModifyAuthorName(auth.Name) == ModifyAuthorName(givenAuthor));
            var books = new List<Book>();

            if (author != null)
            {
                author.Books = _authorBookService.GetAllAuthorBooksBasedOnAuthorId(author.Id).ToList();

                foreach (var book in author.Books)
                {
                    books.Add(_bookService.GetBookById(book.BookId));
                }
            }
            
            return books;
        }

        public async Task CreateAuthor(IFormFile image, string name, string description, string books)
        {
            var bookList = _bookService.GetBooks(books);
            var value = Guid.NewGuid();
            var path = _folder + "\\" + value;
            var imageName = _folder + ".jpg";
            var finalDescription = "No description for this author..";

            if (description != null)
            {
                finalDescription = description;
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

            var author = Author.CreateAuthor(
                name,
                finalDescription,
                path,
                imageName
            );

            _authorRepository.CreateAuthor(author);

            foreach (var book in bookList)
            {
                _authorBookService.CheckAuthorBook(author.Id, book.Id);
            }
        }

        public async Task EditAuthor(Guid id, IFormFile image, string description, string books)
        {
            var authorToBeEdited = _authorRepository.GetAuthorById(id);

            if (authorToBeEdited != null)
            {
                if (description != null)
                {
                    authorToBeEdited.Description = description;
                }

                if (books != null)
                {
                    var bookList = _bookService.GetBooks(books);

                    foreach (var book in bookList)
                    {
                        _authorBookService.CheckAuthorBook(id, book.Id);
                    }
                }

                if (image != null)
                {
                    var path = authorToBeEdited.Folder;
                    authorToBeEdited.ImageName = image.FileName;

                    await _fileManagement.CreateFile(path, image);
                }

                _authorRepository.EditAuthor(authorToBeEdited);
            }
        }

        public void DeleteAuthor(Guid id)
        {
            var authorToBeDeleted = _authorRepository.GetAuthorById(id);

            if (authorToBeDeleted != null)
            {
                _fileManagement.DeleteFolder(authorToBeDeleted.Folder);
                _authorRepository.DeleteAuthor(authorToBeDeleted);
                _authorBookService.DeleteForAuthorId(id);
            }
        }

        public Author GetAuthorById(Guid id)
        {
            var author = _authorRepository.GetAuthorById(id);

            if (author != null)
            {
                author.Books = _authorBookService.GetAllAuthorBooksBasedOnAuthorId(id).ToList();
                author.Posts = _postService.GetAllPosts().Where(post => post.TargetId == author.Id).ToList();

                return author;
            }

            return null;
        }

        private string ModifyAuthorName(string author)
        {
            return author.Replace(" ", "").Replace("-", "").ToLower();
        }
    }
}
