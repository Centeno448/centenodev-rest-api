using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Models.Authorization
{
    public class RefreshToken
    {
        public string AccessToken { get; set; }

        public string UserName { get; set; }

        public string TokenString { get; set; }

        public DateTime ExpireAt { get; set; }
    }
}
