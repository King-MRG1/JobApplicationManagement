           using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Entities;
using JobApplicationManagement.Infrastructure.Persistence;
using JobApplicationManagement.Infrastructure.Repositories;
using JobApplicationManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
namespace JobApplicationManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // ── Controllers ──────────────────────────────────────────────────────────
            builder.Services.AddControllers();
            // ── Database ─────────────────────────────────────────────────────────────
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            // ── Identity ─────────────────────────────────────────────────────────────
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders(); 
            // ── JWT Authentication ────────────────────────────────────────────────────
            var jwtSection = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(
                jwtSection["Key"]
                ?? throw new InvalidOperationException("Jwt:Key is not configured."));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            builder.Services.AddAuthorization();
            // ── Repositories & Services ──────────────────────────────────────────────
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<JobServices>();
            builder.Services.AddScoped<CandidateServices>();
            builder.Services.AddScoped<RecruiterServices>();
            builder.Services.AddScoped<JobCandidateApplicationServices>();
            // Infrastructure services
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<TokenService>();   // also register concrete for AuthController injection
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddHttpContextAccessor();
            // ── OpenAPI ───────────────────────────────────────────────────────────────
            builder.Services.AddOpenApi();
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
