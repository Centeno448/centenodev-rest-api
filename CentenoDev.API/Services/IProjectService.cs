using CentenoDev.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentenoDev.API.Services
{
    public interface IProjectService
    {
        void AddProject(ProjectEntity project);
        void DeleteProject(Guid guid);
        void Dispose();
        Task<ProjectEntity> GetProjectByGuid(Guid guid);
        Task<IEnumerable<ProjectEntity>> GetProjects();
        Task<bool> ProjectExists(Guid guid);
        Task<bool> SaveChangesAsync();
    }
}