﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Models.Lesson
{
    public class Lesson
    {
        public Guid Guid { get; set; }

        public string Content { get; set; }

        public Guid ProjectGuid { get; set; }

    }
}
