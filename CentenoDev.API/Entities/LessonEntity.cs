using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Entities
{
    public class LessonEntity
    {
        [Key]
        public Guid Guid { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

        [ForeignKey("ProjectGuid")]
        public ProjectEntity Project { get; set; }

        public Guid ProjectGuid { get; set; }

    }
}
