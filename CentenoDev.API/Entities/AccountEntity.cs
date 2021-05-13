using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Entities
{
    public class AccountEntity
    {
        [Key]
        public int AccountId { get; set; }

        public Guid AccountGuid { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


        public bool IsAdmin { get; set; } = false;
    }
}
