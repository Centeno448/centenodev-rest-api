using CentenoDev.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentenoDev.API.Services.Lesson
{
    public interface ILessonService
    {
        Task AddLesson(LessonEntity lesson);
        Task DeleteLesson(Guid projectGuid, Guid lessonGuid);
        Task<IEnumerable<LessonEntity>> GetAllLessons(Guid projectGuid);
        Task<LessonEntity> GetLessonByGuid(Guid projectGuid, Guid lessonGuid);
        Task<bool> LessonExists(Guid projectGuid, Guid lessonGuid);
        Task<bool> SaveChangesAsync();
    }
}