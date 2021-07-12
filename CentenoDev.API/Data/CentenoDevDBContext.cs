using CentenoDev.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Data
{
    public class CentenoDevDBContext : DbContext
    {
        public CentenoDevDBContext(DbContextOptions<CentenoDevDBContext> options) : base(options)
        {

        }

        public DbSet<ProjectEntity> Project { get; set; }

        public DbSet<LessonEntity> Lesson { get; set; }
    }
}
