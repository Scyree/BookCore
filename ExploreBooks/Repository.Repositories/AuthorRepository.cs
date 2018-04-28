using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public AuthorRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public Author GetAuthorInfoByDetails(string name, string description)
        {
            return _databaseService.Authors.SingleOrDefault(author => author.Name == name && author.Description == description);
        }

        public IReadOnlyList<Author> GetAllAuthors()
        {
            return _databaseService.Authors.ToList();
        }

        public Author GetAuthorById(Guid id)
        {
            return _databaseService.Authors.SingleOrDefault(author => author.Id == id);
        }

        public void CreateAuthor(Author author)
        {
            _databaseService.Authors.Add(author);

            _databaseService.SaveChanges();
        }

        public void EditAuthor(Author author)
        {
            _databaseService.Authors.Update(author);

            _databaseService.SaveChanges();
        }

        public void DeleteAuthor(Author author)
        {
            _databaseService.Authors.Remove(author);

            _databaseService.SaveChanges();
        }
    }
}
