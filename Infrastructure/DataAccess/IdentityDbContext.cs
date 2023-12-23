using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
	public class IdentityDbContext : DbContext
	{
		public IdentityDbContext()
		{

		}
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
		{

		}

		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }

		public object GetMiddle()
		{
			throw new NotImplementedException();
		}

		public object GetMiddleware()
		{
			throw new NotImplementedException();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);	

		}
	}
}
