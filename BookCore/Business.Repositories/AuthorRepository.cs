using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Persistance;
using Data.Domain.Interfaces.Repositories;

namespace Business.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DatabaseContext _databaseService;

        public AuthorRepository(DatabaseContext databaseService)
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
