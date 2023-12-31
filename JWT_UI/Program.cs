
using Aplication.Services;
using Infrastructure.DataAccess;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Infrastructure;

namespace JWT_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



			//builder.Services.AddControllersWithViews(options =>
			//{
			//	options.Filters.Add<GlobalSampleActionFilter>();
			//});

			// Add services to the container.

            builder.Services.AddIfrastuctureServices(builder.Configuration);
			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




			builder.Services.AddCors();
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("PDP",
                    policy =>
                    {
                        policy.WithOrigins("https://online.pdp.uz")
                        .AllowAnyHeader()
                        .WithMethods("Get", "Put","Post");
                    }
                    );
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(config =>
                {
                    config.SaveToken = true;
                    config.TokenValidationParameters = new()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };


                }
                );
           var app = builder.Build();
            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseCors(builder => builder
            //.WithOrigins(app.Configuration["https://localhost:7235"])
            //.WithMethods("Get")
            //.AllowAnyHeader()
            //);

        //    app.UseCors("PDP");

         //   app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}