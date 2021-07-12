using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Models
{
    public class Project
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GitRepo { get; set; }

        public string ProdLink { get; set; }

        public bool IsPersonal { get; set; }
    }
}
