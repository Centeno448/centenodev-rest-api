using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Models
{
    public class Attachment
    {
        public Guid Guid { get; set; }

        public string Url { get; set; }

        public Guid ProjectGuid { get; set; }

        public string Title { get; set; }
    }
}
