using CentenoDev.API.Data;
using CentenoDev.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Services
{
    public class ProjectService : IProjectService, IDisposable
    {
        private CentenoDevDBContext _context;

        public ProjectService(CentenoDevDBContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ProjectEntity>> GetProjects()
        {
            return await _context.Project.ToListAsync();
        }

        public async Task<ProjectEntity> GetProjectByGuid(Guid guid)
        {
            return await _context.Project.Where(p => p.Guid == guid).FirstOrDefaultAsync();
        }

        public async void AddProject(ProjectEntity project)
        {
            project.Guid = Guid.NewGuid();

            await _context.Project.AddAsync(project);
        }

        public void DeleteProject(Guid guid)
        {
            var project = _context.Project.Where(p => p.Guid == guid).FirstOrDefault();

            _context.Project.Remove(project);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> ProjectExists(Guid guid)
        {
            return await _context.Project.AnyAsync(p => p.Guid == guid);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
