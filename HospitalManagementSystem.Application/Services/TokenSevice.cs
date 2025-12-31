//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//public class TokenService
//{
//    private readonly IConfiguration _config;

//    public TokenService(IConfiguration config)
//    {
//        _config = config;
//    }

//    public string CreateToken(int userId, string role)
//    {
//        var claims = new List<Claim>
//        {
//            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
//            new Claim(ClaimTypes.Role, role)
//        };

//        var key = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
//        );

//        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//        // 👇 THIS IS WHERE YOUR CODE GOES
//        var token = new JwtSecurityToken(
//            issuer: _config["Jwt:Issuer"],
//            audience: _config["Jwt:Audience"],
//            claims: claims,
//            expires: DateTime.UtcNow.AddMinutes(15), // ⏱ token lifetime
//            signingCredentials: creds
//        );

//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }
//}


using HospitalManagementSystem.Application.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

namespace HospitalManagementSystem.Application.Services
{
    public class TokenService
    {
        private readonly JwtSettings _jwt;

        public TokenService(IOptions<JwtSettings> options)
        {
            _jwt = options.Value;
        }

        public string CreateToken(int userId, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.Key)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
