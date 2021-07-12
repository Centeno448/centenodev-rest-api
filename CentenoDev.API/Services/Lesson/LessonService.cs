using CentenoDev.API.Data;
using CentenoDev.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Services.Lesson
{
    public class LessonService : ILessonService
    {
        private readonly CentenoDevDBContext _db;

        public LessonService(CentenoDevDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<LessonEntity>> GetAllLessons(Guid projectGuid)
        {
            return await _db.Lesson.Where(l => l.ProjectGuid == projectGuid).ToListAsync();
        }

        public async Task<LessonEntity> GetLessonByGuid(Guid projectGuid, Guid lessonGuid)
        {
            return await _db.Lesson.Where(l => l.Guid == lessonGuid && l.ProjectGuid == projectGuid).FirstOrDefaultAsync();
        }


        public async Task<bool> LessonExists(Guid projectGuid, Guid lessonGuid)
        {
            var lesson = await _db.Lesson.FindAsync(lessonGuid);

            return lesson != null && lesson.ProjectGuid == projectGuid;
        }
    }
}
