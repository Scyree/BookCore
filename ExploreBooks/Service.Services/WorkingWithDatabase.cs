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
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorMiddleware _authorMiddleware;

        public WorkingWithDatabase(IHostingEnvironment env, IBookRepository bookRepository, IWorkingWithFiles fileManagement, IAuthorBookMiddleware authorBookService, IGenreBookMiddleware genreBookService, IGenreRepository genreRepository, IAuthorMiddleware authorMiddleware)
        {
            _env = env;
            _bookRepository = bookRepository;
            _fileManagement = fileManagement;
            _authorBookService = authorBookService;
            _genreBookService = genreBookService;
            _genreRepository = genreRepository;
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

        private void CreateForSpecificBook(string title, string givenAuthor, string details, string file)
        {
            var checkIfBookAlreadyExists = _bookRepository.GetAllBooks().SingleOrDefault(book => book.Title == title);

            if (checkIfBookAlreadyExists == null)
            {
                var genre = CheckGenre(GetRandomGenre());
                var author = _authorMiddleware.CheckAuthor(givenAuthor);
                var _folder = "books";
                var value = Guid.NewGuid();
                var path = _folder + "\\" + value;
                var imageName = _folder + ".jpg";
                var textContent = File.ReadAllLines(file).ToList();
                var fileName = value + ".txt";
                var destiantionPath = Path.Combine(_env.WebRootPath, "images\\" + path);

                _fileManagement.CopyFile(_folder, value);

                var book = Book.CreateBook(
                    title,
                    "No description for this book at the moment..",
                    path,
                    imageName,
                    "There are mysteries surrounding this book.."
                );

                _bookRepository.CreateBook(book);
                
                _genreBookService.CheckGenreBook(genre.Id, book.Id);
                _authorBookService.CheckAuthorBook(author.Id, book.Id);

                File.WriteAllLines(destiantionPath + "\\" + fileName, textContent.Skip(20));
            }
        }

        public Genre CheckGenre(string description)
        {
            var check = _genreRepository.GetAllGenres().SingleOrDefault(genre => genre.Text == description);

            if (check == null)
            {
                var genre = Genre.CreateGenre(description);
                _genreRepository.CreateGenre(genre);

                return genre;
            }

            return check;
        }
        
        private string GetRandomGenre()
        {
            var random = new Random();
            var genresList = new List<string>
            {
                "SF",
                "Tragedy",
                "Fantasy",
                "Mythology",
                "Adventure",
                "Mystery",
                "Romance",
                "Action",
                "Thriller",
                "Adventure"
            };

            var index = random.Next(0, genresList.Count - 1);

            return genresList[index];

        }
    }
}
