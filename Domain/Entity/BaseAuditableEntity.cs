using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public  class BaseAuditableEntity
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set;}
		public string? LastModifiedBy { get; set; }
	}
}
	

