using System;
using System.Collections.Generic;
using Data.Domain.Entities;
using Middleware.Interfaces;
using Data.Domain.Interfaces.Repositories;


namespace Middleware.Services
{
    public class AuthorBookService : IAuthorBookService
    {
        private readonly IAuthorBookRepository _repository;

        public AuthorBookService(IAuthorBookRepository repository)
        {
            _repository = repository;
        }

        public void CheckAuthorBook(Guid authorId, Guid bookId)
        {
            var check = _repository.GetAuthorBookById(authorId, bookId);

            if (check == null)
            {
                var authorBook = AuthorBook.CreateAuthorBook(authorId, bookId);
                _repository.CreateAuthorBook(authorBook);
            }
        }

        public List<AuthorBook> GetAllAuthorBooksBasedOnBookId(Guid bookId)
        {
            return _repository.GetAllAuthorBooksBasedOnBookId(bookId);
        }

        public List<AuthorBook> GetAllAuthorBooksBasedOnAuthorId(Guid authorId)
        {
            return _repository.GetAllAuthorBooksBasedOnAuthorId(authorId);
        }
    }
}
