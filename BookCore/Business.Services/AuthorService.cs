using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Domain.Interfaces.Services;

namespace Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        
        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public Author CheckAuthor(string description)
        {
            var check = _repository.GetAllAuthors().SingleOrDefault(author => author.Name == description);

            if (check == null)
            {
                var author = Author.CreateAuthor(description, "Momentan nu exista o descriere a acestui autor");
                _repository.CreateAuthor(author);

                return author;
            }

            return check;
        }

        public List<Author> GetAuthors(string description)
        {
            var bruteAuthor = description.Replace(" ", "");
            var authors = bruteAuthor.Split(",");
            var authorList = new List<Author>();

            foreach (var author in authors)
            {
                authorList.Add(CheckAuthor(author));
            }

            return authorList;
        }

        public Author GetAuthorBasedOnId(Guid id)
        {
            return _repository.GetAuthorById(id);
        }
    }
}
