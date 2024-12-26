using LoanAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace LoanAPI.Helpers
{

    public class JwtHelper
    {
        private readonly string _key;

        public JwtHelper(string key)
        {
            _key = key;
        }

        public string GenerateToken(User user, string role)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, role)
                };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
