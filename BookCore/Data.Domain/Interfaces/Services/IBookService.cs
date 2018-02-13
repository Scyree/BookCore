using System;

namespace Data.Domain.Interfaces.Services
{
    public interface IBookService
    {
        void DeleteFileForGivenId(Guid id);
    }
}
