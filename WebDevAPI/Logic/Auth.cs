using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Logic
{
    public class Auth
    {
        public IConfiguration _configuration;

        public Auth(IConfiguration config)
        {
            _configuration = config;
        }

        public string CreateToken(User user, List<Claim> claims)
        {
            //Is this Secure?
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        //RefreshToken?
    }
}
