using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IAuthorMiddleware
    {
        Author GetAuthorInfoByDetails(string name, string description);
        Author CheckAuthor(string description);
        List<Author> GetAuthors(string description);
    }
}
