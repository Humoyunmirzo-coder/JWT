using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entity
{
       public  class User
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public virtual IEnumerable<Role> Roles { get; set;}

    }
}
