using Domain.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
	public  class interceptor : SaveChangesInterceptor

	{

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateEntities((IdentityDbContext?)eventData.Context);

			return base.SavingChanges(eventData, result);
		}
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdateEntities((IdentityDbContext?)eventData.Context);

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		

		private void UpdateEntities(IdentityDbContext?  context)
		{
			if (context == null) return;

			foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
			{
				if (entry.State == EntityState.Added ||
					entry.State == EntityState.Modified ||
					entry.State == EntityState.Deleted)
				{
					var auditlog = new Auditlog()
					{
						EntityName = entry.Entity.GetType().Name,
						Date = DateTime.UtcNow,
						OperationType = Domain.Entity.AuditEnnum.OperationType.Addet,
						UpdateVelueJson = entry.CurrentValues.ToObject().ToString(),
						UserName = "Najim",
						Id = entry.Entity.Id

					};
					context.Add(auditlog);
				}
			
			}
		}


	}
	public static class Extensions
	{
		public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
			entry.References.Any(r =>
				r.TargetEntry != null &&
				r.TargetEntry.Metadata.IsOwned() &&
				(r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
	}
}
