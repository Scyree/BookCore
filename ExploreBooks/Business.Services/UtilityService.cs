using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;

namespace Business.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IBookStateRepository _stateRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public UtilityService(IBookRepository bookRepository, IAuthorRepository authorRepository, IPostRepository postRepository, ICommentRepository commentRepository, IBookStateRepository stateRepository, IApplicationUserRepository applicationUserRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _stateRepository = stateRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        public IReadOnlyList<BookState> GetAllBooksForUserId(string userId)
        {
            var books = _stateRepository.GetAllBookStates().Where(state => state.UserId.ToString() == userId);
            var bookActivity = new List<BookState>();

            foreach (var book in books)
            {
                bookActivity.Add(book);
            }
            
            return bookActivity.ToList();
        }

        public IReadOnlyList<BookState> GetFirstNBooksForUserId(string userId, int number)
        {
            var books = _stateRepository.GetAllBookStates().Where(state => state.UserId.ToString() == userId);
            var bookActivity = new List<BookState>();

            foreach (var book in books)
            {
                bookActivity.Add(book);
            }

            if (bookActivity.Count < number)
            {
                return bookActivity;
            }

            return bookActivity.Take(number).ToList();
        }

        public IReadOnlyList<Post> GetAllPostsForUser(string userId)
        {
            var posts = _postRepository.GetAllPosts().Where(post => post.UserId.ToString() == userId);
            var postActivity = new List<Post>();

            foreach (var post in posts)
            {
                postActivity.Add(post);
            }

            return postActivity;
        }

        public IReadOnlyList<Post> GetFirstNPostsForUserId(string userId, int number)
        {
            var posts = _postRepository.GetAllPosts().Where(post => post.UserId.ToString() == userId);
            var postActivity = new List<Post>();

            foreach (var post in posts)
            {
                postActivity.Add(post);
            }

            if (postActivity.Count < number)
            {
                return postActivity;
            }

            return postActivity.Take(number).ToList();
        }
        
        public Book GetBookById(Guid bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            
            return book;
        }

        public Author GetAuthorById(Guid authorId)
        {
            var author = _authorRepository.GetAuthorById(authorId);

            return author;
        }

        public Post GetPostById(Guid postId)
        {
            var post = _postRepository.GetPostById(postId);

            return post;
        }

        public Comment GetCommentById(Guid commentId)
        {
            var comment = _commentRepository.GetCommentById(commentId);

            return comment;
        }

        public BookState GetBookStateById(Guid bookId, Guid userId)
        {
            var bookState = _stateRepository.GetAllBookStates().SingleOrDefault(bookstate => bookstate.UserId == userId && bookstate.TargetId == bookId);

            return bookState;
        }

        public ApplicationUser GetApplicationUser(string userId)
        {
            var applicationUser = _applicationUserRepository.GetAllApplicationUsers().SingleOrDefault(user => user.Id == userId);

            return applicationUser;
        }

        public string DisplayDate(DateTime date)
        {
            return "BAKA";
        }
    }
}
