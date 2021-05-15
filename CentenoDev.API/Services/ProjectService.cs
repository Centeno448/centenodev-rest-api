using CentenoDev.API.Data;
using CentenoDev.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Services
{
    public class ProjectService : IProjectService
    {
        private CentenoDevDBContext _db;

        public ProjectService(CentenoDevDBContext context)
        {
            _db = context;
        }


        public async Task<IEnumerable<ProjectEntity>> GetProjects()
        {
            return await _db.Project.ToListAsync();
        }

        public async Task<ProjectEntity> GetProjectByGuid(Guid guid)
        {
            return await _db.Project.FindAsync(guid);
        }

        public async Task AddProject(ProjectEntity project)
        {
            project.Guid = Guid.NewGuid();

            await _db.Project.AddAsync(project);
        }

        public async Task DeleteProject(Guid guid)
        {
            var project = await _db.Project.FindAsync(guid);

            _db.Project.Remove(project);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync() > 0);
        }

        public async Task<bool> ProjectExists(Guid guid)
        {
            return await _db.Project.FindAsync(guid) != null;
        }


    }
}
