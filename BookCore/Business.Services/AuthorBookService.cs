using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;

namespace Business.Services
{
    public class AuthorBookService
    {
        private readonly IAuthorBookRepository _repository;

        public AuthorBookService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public AuthorBook CheckAuthor(string description)
        {
            var check = _repository.GetAllBooksBasedOnAuthorId().SingleOrDefault(authorBook => authorBook.Name == description);

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
    }
}
