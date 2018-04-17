using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Data.Domain.Interfaces.Services
{
    public interface IAuthorService
    {
        Author CheckAuthor(string description);
        List<Author> GetAuthors(string description);
        Author GetAuthorBasedOnId(Guid id);
    }
}
