using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IWorkingWithDatabase
    {
        void PopulateTextFiles();
        List<string> GetTextfileForBook(Guid bookId);
    }
}
