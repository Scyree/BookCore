﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Data.Domain.Interfaces.Services
{
    public interface IWorkingWithFiles
    {
        Task CreateFile(string folder, Guid value, IFormFile image);
        void DeleteFolderForGivenId(string folder, Guid value);
        void DeleteFileForGivenId(string folder, Guid value, string fileName);
        string GetPath(string folder, Guid value);
    }
}
