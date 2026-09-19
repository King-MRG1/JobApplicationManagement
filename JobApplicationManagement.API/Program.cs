
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain;
using JobApplicationManagement.Infrastructure.Persistence;
using JobApplicationManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace JobApplicationManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");

            builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(connectionString));

            builder.Services.AddScoped<JobServices>();
            builder.Services.AddScoped<IGenericRepository<Job>, GenericRepository<Job>>();

            builder.Services.AddOpenApi();

            var app = builder.Build();  

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
