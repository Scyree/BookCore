﻿using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IUtilityService
    {
        IReadOnlyList<BookState> GetAllBooksForUserId(string userId);
        IReadOnlyList<BookState> GetFirstNBooksForUserId(string userId, int number);
        IReadOnlyList<Post> GetAllPostsForUser(string userId);
        IReadOnlyList<Post> GetFirstNPostsForUserId(string userId, int number);
        Book GetBookById(Guid id);
        Author GetAuthorById(Guid id);
        Post GetPostById(Guid id);
        Comment GetCommentById(Guid commentId);
        BookState GetBookStateById(Guid bookId, Guid userId);
        ApplicationUser GetApplicationUser(string userId);
    }
}
