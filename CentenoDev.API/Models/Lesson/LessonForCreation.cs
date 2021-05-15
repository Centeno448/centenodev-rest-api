using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Models.Lesson
{
    public class LessonForCreation
    {
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }
    }
}
