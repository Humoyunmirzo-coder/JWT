using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public  class BaseAuditableEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
	
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set;}

		public DateTimeOffset Created { get; set; }

		public string? CreatedBy { get; set; }

		public DateTimeOffset LastModified { get; set; }

		public string? LastModifiedBy { get; set; }
	}
}
	

