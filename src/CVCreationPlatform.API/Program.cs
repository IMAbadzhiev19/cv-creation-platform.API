using CVCreationPlatform.AuthService.Contracts;
using CVCreationPlatform.AuthService.Implementations;
using Data.Data;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped<UserService>();
			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}