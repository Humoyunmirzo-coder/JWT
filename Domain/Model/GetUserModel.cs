﻿using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public  class GetUserModel
    {
       public  Token token { get; set; }
       public  User  user { get; set; }
    }
}
