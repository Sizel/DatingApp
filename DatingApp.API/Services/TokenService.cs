using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Services
{
    public class TokenService : ITokenService
    {
        IConfiguration config;
        UserManager<User> userManager;
        
        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }

        public async Task<string> CreateJwtToken(User user, int daysValid) {
            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = claims;
            tokenDescriptor.Expires = DateTime.UtcNow.Date.AddDays(daysValid);
            tokenDescriptor.SigningCredentials = 
            new SigningCredentials(
                new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(config.GetValue<string>("privateKey"))), 
            SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}