using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Entities
{
    public class AttachmentEntity
    {
        [Key]
        public Guid Guid { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }

        [ForeignKey("ProjectGuid")]
        public ProjectEntity Project { get; set; }

        public Guid ProjectGuid { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
    }
}
