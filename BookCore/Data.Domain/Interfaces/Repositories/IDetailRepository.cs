using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IDetailRepository
    {
        IReadOnlyList<Detail> GetAllDetails();
        Detail GetDetailById(Guid id);
        void CreateDetail(Detail detail);
        void EditDetail(Detail detail);
        void DeleteDetail(Detail detail);
    }
}
