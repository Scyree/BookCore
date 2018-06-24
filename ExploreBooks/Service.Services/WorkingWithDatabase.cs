using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Data;
using Microsoft.AspNetCore.Hosting;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class WorkingWithDatabase : IWorkingWithDatabase
    {
        private readonly IHostingEnvironment _env;
        private readonly IBookRepository _bookRepository;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly IAuthorBookMiddleware _authorBookService;
        private readonly IGenreBookMiddleware _genreBookService;
        private readonly IGenreMiddleware _genreMiddleware;
        private readonly IAuthorMiddleware _authorMiddleware;

        public WorkingWithDatabase(IHostingEnvironment env, IBookRepository bookRepository, IWorkingWithFiles fileManagement, IAuthorBookMiddleware authorBookService, IGenreBookMiddleware genreBookService, IGenreMiddleware genreMiddleware, IAuthorMiddleware authorMiddleware)
        {
            _env = env;
            _bookRepository = bookRepository;
            _fileManagement = fileManagement;
            _authorBookService = authorBookService;
            _genreBookService = genreBookService;
            _genreMiddleware = genreMiddleware;
            _authorMiddleware = authorMiddleware;
        }
        
        public void PopulateTextFiles()
        {
            var path = Path.Combine(_env.WebRootPath, "rawtext");
            var listOfFiles = Directory.GetFiles(path);

            foreach (var file in listOfFiles)
            {
                var content = File.ReadLines(file).ToList();
                var title = content.FirstOrDefault(line => line.Contains("Title")).Replace("Title: ", "");
                var author = content.FirstOrDefault(line => line.Contains("Author")).Replace("Author: ", "");
                var releaseDate = content.FirstOrDefault(line => line.Contains("Release Date"));
                
                CreateForSpecificBook(title, author, releaseDate, file);
            }

        }

        public List<string> GetTextfileForBook(Guid bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            var fileName = "";
            var path = Path.Combine(_env.WebRootPath, "images/" + book.Folder);
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);

                if (extension == ".txt")
                {
                    fileName = file;
                }
            }

            var content = File.ReadLines(fileName).ToList();

            return content;
        }

        private void CreateForSpecificBook(string title, string givenAuthor, string details, string file)
        {
            var checkIfBookAlreadyExists = _bookRepository.GetBookBasedOnTitle(title);

            if (checkIfBookAlreadyExists == null)
            {
                var genre = _genreMiddleware.CheckGenre(_genreMiddleware.GetRandomGenre());
                var author = _authorMiddleware.CheckAuthor(givenAuthor);
                var _folder = "books";
                var value = Guid.NewGuid();
                var path = _folder + "\\" + value;
                var imageName = _folder + ".jpg";
                var textContent = File.ReadAllLines(file).Skip(20).ToList();
                var fileName = value + ".txt";
                var destiantionPath = Path.Combine(_env.WebRootPath, "images\\" + path);

                _fileManagement.CopyFile(_folder, value);
                File.WriteAllLines(destiantionPath + "\\" + fileName, textContent);

                var book = Book.CreateBook(
                    title,
                    "No description for this book at the moment..",
                    path,
                    imageName,
                    details
                );

                book.Pages = textContent.Count / 35;

                _bookRepository.CreateBook(book);
                _genreBookService.CheckGenreBook(genre.Id, book.Id);
                _authorBookService.CheckAuthorBook(author.Id, book.Id);
            }
        }
    }
}
