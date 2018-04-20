using System;
using System.Linq;
using System.Collections.Generic;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Middleware.Interfaces;

namespace Middleware.Services
{
    public class AuthorGeneralUsage : IAuthorGeneralUsage
    {
        private readonly IAuthorRepository _repository;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly string _folder;

        public AuthorGeneralUsage(IAuthorRepository repository, IWorkingWithFiles fileManagement)
        {
            _repository = repository;
            _fileManagement = fileManagement;
            _folder = "Authors";
        }

        public Author GetAuthorInfoByDetails(string name, string description)
        {
            return _repository.GetAuthorInfoByDetails(name, description);
        }

        public IReadOnlyList<Author> GetAllAuthors()
        {
            return _repository.GetAllAuthors();
        }

        public Author GetAuthorById(Guid id)
        {
            return _repository.GetAuthorById(id);
        }

        public void CreateAuthor(Author author)
        {
            _repository.CreateAuthor(author);
        }

        public void EditAuthor(Author author)
        {
            _repository.EditAuthor(author);
        }

        public void DeleteAuthor(Author author)
        {
            _repository.DeleteAuthor(author);
            _fileManagement.DeleteFolder(author.Folder);
        }
        
        public Author CheckAuthor(string description)
        {
            var check = _repository.GetAllAuthors().SingleOrDefault(author => author.Name == description);

            if (check == null)
            {
                var value = Guid.NewGuid();
                var path = _folder + "\\" + value;
                var imageName = _folder + ".jpg";

                _fileManagement.CopyFile(_folder, value);

                var author = Author.CreateAuthor(
                    description,
                    "Momentan nu exista o descriere a acestui autor",
                    path,
                    imageName
                );

                _repository.CreateAuthor(author);

                return author;
            }

            return check;
        }

        public List<Author> GetAuthors(string description)
        {
            var authors = description.Split(",");
            var authorList = new List<Author>();

            foreach (var author in authors)
            {
                authorList.Add(CheckAuthor(author));
            }

            return authorList;
        }
    }
}
