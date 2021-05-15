using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Models.Lesson
{
    public class Lesson
    {
        public int LessonId { get; set; }

        public Guid Guid { get; set; }

        public string Content { get; set; }

        public int ProjectId { get; set; }

    }
}
