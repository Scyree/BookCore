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
        private readonly ICommentService _commentService;

        public PostService(IPostMiddleware postService, IPostRepository postRepository, ICommentService commentService)
        {
            _postService = postService;
            _postRepository = postRepository;
            _commentService = commentService;
        }
        
        public IReadOnlyList<Post> GetOnlyFirstNumberOfPosts(int number)
        {
            return _postService.GetPostsBasedOnLikes().Take(number).ToList();
        }
        
        public IReadOnlyList<Post> GetAllPostsForTargetId(Guid targetId)
        {
            var posts = _postService.GetPostsByDate().Where(post => post.TargetId == targetId).ToList();

            foreach (var post in posts)
            {
                post.Comments = _commentService.GetAllComments(post.Id).ToList();
            }

            return posts;
        }

        public IReadOnlyList<Post> GetAllPosts()
        {
            var posts = _postService.GetPostsByDate();

            foreach (var post in posts)
            {
                post.Comments = _commentService.GetAllComments(post.Id).ToList();
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

        //private void DeleteNegativePosts(Guid postId)
        //{
        //    if (GetNumberOfLikes(postId) <= -20)
        //    {
        //        var likeList = _likeService.GetAllLikes().Where(likes => likes.TargetId == postId).ToList();

        //        foreach (var like in likeList)
        //        {
        //            _likeService.DeleteLike(like);
        //        }

        //        _PostService.DeletePost(_postService.GetPostById(postId));
        //    }
        //}
    }
}
