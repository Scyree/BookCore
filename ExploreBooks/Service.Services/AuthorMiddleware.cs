using System;
using System.Linq;
using System.Collections.Generic;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class AuthorMiddleware : IAuthorMiddleware
    {
        private readonly IAuthorRepository _repository;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly string _folder;

        public AuthorMiddleware(IAuthorRepository repository, IWorkingWithFiles fileManagement)
        {
            _repository = repository;
            _fileManagement = fileManagement;
            _folder = "authors";
        }

        public Author GetAuthorInfoByDetails(string name, string description)
        {
            return _repository.GetAuthorInfoByDetails(name, description);
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
                    "No description for this author..",
                    path,
                    imageName
                );

                _repository.CreateAuthor(author);

                return author;
            }

            return check;
        }

        public IReadOnlyList<Author> GetAuthors(string description)
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
