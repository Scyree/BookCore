using Repository.Interfaces;
using Domain.Data;
using Domain.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Repository.Repositories
{
    public class AuthorBookRepository : IAuthorBookRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public AuthorBookRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public List<AuthorBook> GetAllAuthorBooksBasedOnBookId(Guid bookId)
        {
            return _databaseService.AuthorBooks.Where(author => author.BookId == bookId).ToList();
        }

        public List<AuthorBook> GetAllAuthorBooksBasedOnAuthorId(Guid authorId)
        {
            return _databaseService.AuthorBooks.Where(book => book.AuthorId == authorId).ToList();
        }

        public AuthorBook GetAuthorBookById(Guid authorId, Guid bookId)
        {
            return _databaseService.AuthorBooks.SingleOrDefault(authorBook => authorBook.AuthorId == authorId && authorBook.BookId == bookId);
        }

        public void CreateAuthorBook(AuthorBook authorBook)
        {
            _databaseService.AuthorBooks.Add(authorBook);

            _databaseService.SaveChanges();
        }

        public void EditAuthorBook(AuthorBook authorBook)
        {
            _databaseService.AuthorBooks.Update(authorBook);

            _databaseService.SaveChanges();
        }

        public void DeleteAuthorBook(AuthorBook authorBook)
        {
            _databaseService.AuthorBooks.Remove(authorBook);

            _databaseService.SaveChanges();
        }
    }
}
