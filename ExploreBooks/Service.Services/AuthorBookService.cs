using Service.Interfaces;
using System;
using System.Collections.Generic;
using Domain.Data;
using Repository.Interfaces;

namespace Service.Services
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

        public void DeleteForAuthorId(Guid authorId)
        {
            var authorBooks = _repository.GetAllAuthorBooksBasedOnAuthorId(authorId);

            foreach (var authorBook in authorBooks)
            {
                _repository.DeleteAuthorBook(authorBook);
            }
        }

        public void DeleteForBookId(Guid bookId)
        {
            var authorBooks = _repository.GetAllAuthorBooksBasedOnBookId(bookId);

            foreach (var authorBook in authorBooks)
            {
                _repository.DeleteAuthorBook(authorBook);
            }
        }
    }
}
