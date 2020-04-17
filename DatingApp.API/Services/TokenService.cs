using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Services
{
    public class TokenService : ITokenService
    {
        IConfiguration _config;
        
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> CreateJwtToken(User user, int daysValid) {
            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Name));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = claims;
            tokenDescriptor.Expires = DateTime.UtcNow.Date.AddDays(daysValid);
            tokenDescriptor.SigningCredentials = 
            new SigningCredentials(
                new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(_config.GetValue<string>("privateKey"))), 
            SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            
            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}