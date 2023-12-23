using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public  class RefreshToken
    {
        [Key]
        public int  Id {  get; set; }
        public int  UserId { get; set; }
        public User User { get; set; }
        public string  RefreshTokenValue { get; set; }
        public DateTime ExpireTime { get; set; }

       
    }
}
