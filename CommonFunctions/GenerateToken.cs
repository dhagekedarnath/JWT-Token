using Microsoft.IdentityModel.Tokens;
using ReactCrudAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactCrudAPI.CommonFunctions
{
    public static class GenerateToken
    {
        
        public static string GetToken(UserDetail userDetail, IConfiguration _configuration)
        {
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("UserId",userDetail.Id.ToString()),
                new Claim("DisplayName",userDetail.FullName),
                new Claim("UserName",userDetail.FullName),
                new Claim("Email",userDetail.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                Claims,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: signIn);

            var Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
    }
}
