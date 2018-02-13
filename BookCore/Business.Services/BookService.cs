using Data.Domain.Interfaces.Repositories;
using Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IHostingEnvironment _env;

        public BookService(IBookRepository repository, IHostingEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public void DeleteFileForGivenId(Guid id)
        {
            var book = _repository.GetBookById(id);
            var searchedPath = _env.WebRootPath + book.Folder.Replace("~","");

            if (Directory.Exists(searchedPath))
            {
                Directory.Delete(searchedPath, true);
            }

            _repository.DeleteBook(book);
        }

    }
}
