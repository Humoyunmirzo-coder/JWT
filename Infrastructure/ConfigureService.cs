using Aplication.Services;
using Infrastructure.DataAccess;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public  static class ConfigureService
	{
		public static void AddIfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<ISaveChangesInterceptor, interceptor>();
		    services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IIdentityServise, IdentityService>();

			services.AddDbContext<IdentityDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("ConnetionString")));

		}
	}
}
