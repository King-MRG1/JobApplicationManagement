using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace JobApplicationManagement.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <inheritdoc />
        public string GenerateToken(ApplicationUser user, IList<string> roles)
            => GenerateToken(user, roles, recruiterId: null, candidateId: null);
        /// <summary>
        /// Generates a JWT with an optional <c>RecruiterId</c> custom claim.
        /// The claim is read back by <see cref="CurrentUserService"/> on subsequent requests.
        /// </summary>
        public string GenerateToken(ApplicationUser user, IList<string> roles, int? recruiterId, int? candidateId)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSection["Key"]
                    ?? throw new InvalidOperationException("Jwt:Key is not configured.")));
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expirationMinutes = int.Parse(jwtSection["ExpirationMinutes"] ?? "60");
            var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName ?? string.Empty)
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            if (recruiterId.HasValue)
                claims.Add(new Claim("RecruiterId", recruiterId.Value.ToString()));
            else if(candidateId.HasValue)
                claims.Add(new Claim("CandidateId", candidateId.Value.ToString()));
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}