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

        public string CreateToken(Guid id, List<Claim> claims)
        {
            //Is this Secure?
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: id.ToString(),
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(token);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }

        public JwtSecurityToken ValidateToken(string token)
        {
            //Is this Secure?
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var tokenHandler = new JwtSecurityTokenHandler();
           
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key
                }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
       
        }

        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

        //RefreshToken?
    }
}
