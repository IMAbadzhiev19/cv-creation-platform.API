using CVCreationPlatform.AuthService.Contracts;
using CVCreationPlatform.AuthService.Implementations;
using Data.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace CVCreationPlatform.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Configuration.AddJsonFile("appsettings.json");

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseSqlServer(connectionString)
				.Options;

            builder.Services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(connectionString, builder =>
					{
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        builder.MigrationsAssembly("CVCreationPlatform.API");
                    })
                );
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IJWTService, JWTService>();
			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				options.OperationFilter<SecurityRequirementsOperationFilter>();
			});

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options => {
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value!,
                        ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Value!,
                        ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
							.GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value!)),
						ValidateIssuer = true,
						ValidateAudience = true, 
						ValidateLifetime = true,
					};
				});

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}