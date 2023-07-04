using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebDevAPI.Db.Models;
using System.Security.Cryptography;

namespace WebDevAPI.Logic
{
    public class Auth
    {
        public IConfiguration _configuration;

        public Auth(IConfiguration config)
        {
            _configuration = config;
        }

        public string CreateToken(string id, List<Claim> claims)
        {
            //Is this Secure?
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: id,
                claims: claims,
                expires: DateTime.Now.AddMinutes(320),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                    ValidateLifetime = false,
                    IssuerSigningKey = key
                }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
       
        }

        public string RefreshToken(string token, List<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration.GetSection("AppSettings:Token").Value!));

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                IssuerSigningKey = key
            }, out SecurityToken validatedToken);

            if(validatedToken.ValidTo > DateTime.Now)
            {
                return "Token has not expires yet";
            }

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var refreshedtoken = new JwtSecurityToken(
                issuer: validatedToken.Id,
                claims: claims,
                expires: DateTime.Now.AddMinutes(320),
                signingCredentials: creds
                );
            return tokenHandler.WriteToken(refreshedtoken);
        }

        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

        //Input should be emails, message etc
        public async Task SendMailAsync(string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("s1144640@student.windesheim.nl", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("s1144640@student.windesheim.nl", "Example User");
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public string GenerateRandomString(int length, string allowableChars = null)
        {
            if ( string.IsNullOrEmpty(allowableChars) )
            {
                allowableChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            var rnd = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(rnd);

            var allowable = allowableChars.ToCharArray();
            var l = allowable.Length;
            var chars = new char[length]; ;
            for (int i = 0;i < length; i++)
                chars[i] = allowable[rnd[i] % l];

            return new string(chars);
        }

        //RefreshToken?
    }
}
