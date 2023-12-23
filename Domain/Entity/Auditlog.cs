using Domain.Entity.AuditEnnum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public  class Auditlog
	{
		[Key]
		public int Id { get; set; }	
		public string EntityName { get; set; }
		public DateTime Date { get; set; }
		public EntityState OperationType { get; set; }
        public string UpdateVelueJson { get; set; }
		public string? UserName { get; set; }


    }
}
