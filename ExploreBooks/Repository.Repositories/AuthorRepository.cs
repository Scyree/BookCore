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

        public Author GetAuthorBasedOnName(string name)
        {
            return _databaseService.Authors.FirstOrDefault(author => author.Name.Replace(" ", "").Replace("-", "").ToLower() == name);
        }

        public Author GetAuthorInfoByDetails(string name, string description)
        {
            return _databaseService.Authors.FirstOrDefault(author => author.Name == name && author.Description == description);
        }

        public List<Author> GetAllAuthors()
        {
            return _databaseService.Authors.ToList();
        }

        public List<Author> GetFirstNAuthors(int skipNumber, int takeNumber)
        {
            return _databaseService.Authors.OrderByDescending(author => author.Name).Skip(skipNumber).Take(takeNumber).ToList();
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
