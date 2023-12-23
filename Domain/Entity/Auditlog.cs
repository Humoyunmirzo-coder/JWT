﻿using Domain.Entity.AuditEnnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public  class Auditlog
	{
		public int Id { get; set; }	
		public string EntityName { get; set; }
		public DateTime Date { get; set; }
		public OperationType OperationType { get; set; }
        public string UpdateVelueJson { get; set; }
		public string? UserName { get; set; }


    }
}
