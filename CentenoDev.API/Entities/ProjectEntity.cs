using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Entities
{
    public class ProjectEntity
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description_EN { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description_ES { get; set; }

        [MaxLength(500)]
        public string GitRepo { get; set; }
        
        [MaxLength(500)]
        public string ProdLink { get; set; }

        [Required]
        public bool IsPersonal { get; set; } = false;


        public ICollection<LessonEntity> Lessons { get; set; } = new List<LessonEntity>();
    }
}
