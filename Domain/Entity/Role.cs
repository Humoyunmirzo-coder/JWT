using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public  class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
      
       
        public virtual ICollection<User > User { get; set; }

        public virtual ICollection<Permission > Permissions { get; set; }
    }
}
