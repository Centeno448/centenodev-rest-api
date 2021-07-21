using CentenoDev.API.Data;
using CentenoDev.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentenoDev.API.Extensions;

namespace CentenoDev.API.Services.Project
{
    public class ProjectService : IProjectService
    {
        private CentenoDevDBContext _db;

        public ProjectService(CentenoDevDBContext context)
        {
            _db = context;
        }


        public async Task<IEnumerable<ProjectEntity>> GetProjects(bool random, int limit)
        {
            var projects = await _db.Project.ToListAsync();


            if (random)
            {
                projects.Shuffle();
            }

            if(limit > 0 && limit < projects.Count)
            {
                return projects.Take(limit);
            }

            return projects;
        }

        public async Task<ProjectEntity> GetProjectByGuid(Guid guid)
        {
            return await _db.Project.FindAsync(guid);
        }

        

        public async Task<bool> ProjectExists(Guid guid)
        {
            return await _db.Project.FindAsync(guid) != null;
        }


    }
}
