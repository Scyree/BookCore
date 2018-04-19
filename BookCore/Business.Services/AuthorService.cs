using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IAuthorRepository _repository;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly string _folder;

        public AuthorService(IAuthorRepository repository, IWorkingWithFiles fileManagement, IAuthorBookRepository authorBookRepository)
        {
            _repository = repository;
            _fileManagement = fileManagement;
            _authorBookRepository = authorBookRepository;
            _folder = "Authors";
        }

        public IReadOnlyList<Author> GetAllAuthors()
        {
            var authors = _repository.GetAllAuthors();

            foreach (var author in authors)
            {
                author.Books = _authorBookRepository.GetAllAuthorBooksBasedOnAuthorId(author.Id);
            }

            return authors;
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
        
        public async Task CreateAuthor(IFormFile image, string name, string description)
        {
            var value = Guid.NewGuid();
            var path = _folder + "\\" + value;
            var imageName = _folder + ".jpg";

            await _fileManagement.CreateFile(_folder, value, image);

            if (image != null)
            {
                imageName = image.FileName;

                await _fileManagement.CreateFile(_folder, value, image);
            }
            else
            {
                _fileManagement.CopyFile(_folder, value);
            }

            var author = Author.CreateAuthor(
                description,
                "Momentan nu exista o descriere a acestui autor",
                path,
                imageName
            );

            _repository.CreateAuthor(author);
        }

        public async Task EditAuthor(Guid id, IFormFile image, string name, string description)
        {
            var authorToBeEdited = _repository.GetAuthorById(id);

            authorToBeEdited.Name = name;
            authorToBeEdited.Description = description;

            if (image != null)
            {
                var value = Guid.NewGuid();

                await _fileManagement.CreateFile(_folder, value, image);
            }

            _repository.EditAuthor(authorToBeEdited);
        }

        public void DeleteAuthor(Guid id)
        {
            _fileManagement.DeleteFolderForGivenId(_folder, id);
        }

        public Author GetAuthorById(Guid id)
        {
            return _repository.GetAuthorById(id);
        }
    }
}
