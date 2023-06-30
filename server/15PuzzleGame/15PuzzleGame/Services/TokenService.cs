using _15PuzzleGame.Entities;
using _15PuzzleGame.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _15PuzzleGame.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
        public TokenService(IConfiguration configuration)
        {
            _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["TokenKey"])
                );
        }
        public string CreateToken(MyUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var creds = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
