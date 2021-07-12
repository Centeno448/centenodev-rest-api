using CentenoDev.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentenoDev.API.Services.Project
{
    public interface IProjectService
    {
        Task<ProjectEntity> GetProjectByGuid(Guid guid);
        Task<IEnumerable<ProjectEntity>> GetProjects();
        Task<bool> ProjectExists(Guid guid);
    }
}