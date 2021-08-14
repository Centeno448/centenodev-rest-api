using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Models
{
    public class Lesson
    {
        public Guid Guid { get; set; }

        public string Content_EN { get; set; }

        public string Content_ES { get; set; }

        public Guid ProjectGuid { get; set; }

    }
}
