using System;
using System.Linq;
using System.Collections.Generic;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostMiddleware _postService;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public PostService(IPostMiddleware postService, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postService = postService;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        
        public List<Post> GetOnlyFirstNumberOfPosts(int number)
        {
            return _postService.GetPostsByDate().Take(number).ToList();
        }
        
        public List<Post> GetAllPostsForTargetId(Guid targetId)
        {
            var posts = _postRepository.GetAllPostsForTargetId(targetId);

            foreach (var post in posts)
            {
                post.Comments = _commentRepository.GetAllCommentsGivenPostIdSortedByDate(post.Id);
            }

            return posts;
        }

        public List<Post> GetAllPosts()
        {
            var posts = _postService.GetPostsByDate();

            foreach (var post in posts)
            {
                post.Comments = _commentRepository.GetAllCommentsGivenPostIdSortedByDate(post.Id);
            }

            return posts;
        }

        public void CreatePost(Guid userId, Guid targetId, string description)
        {
            var post = Post.CreatePost(
                description,
                userId,
                targetId
            );

            _postRepository.CreatePost(post);
        }

        public void EditPost(Guid id, string description)
        {
            var postToBeEdited = _postRepository.GetPostById(id);

            if (postToBeEdited != null)
            {
                if (description != null)
                {
                    postToBeEdited.Description = description;
                }
                
                _postRepository.EditPost(postToBeEdited);
            }
        }

        public void DeletePost(Guid id)
        {
            var postToBeDeleted = _postRepository.GetPostById(id);

            if (postToBeDeleted != null)
            {
                _postRepository.DeletePost(postToBeDeleted);
            }
        }

        public Post GetPostById(Guid postId)
        {
            return _postRepository.GetPostById(postId);
        }
    }
}
