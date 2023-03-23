using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GetPlayerDto>> Register(PostRegisterUserDto request)
        {

            var requestedUser = await UserRepository.TryFind(e => e.Email == request.Email);
            if (requestedUser.succes) return BadRequest("User already exists");

            var user = new User
            {
                Id = new Guid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await UserRepository.Create(user);

            return Ok(user.GetUserDto());
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(PostLoginUserDto request)
        {
            var findUser = await UserRepository.TryFind(e => e.Email == request.Email);
            var result = findUser.result;
            if (result == null) return NotFound();


            if (result.Email != request.Email)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, result.PasswordHash))
            {
                return BadRequest("Email or password is wrong.");
            }

            string token = CreateToken(result);

            return Ok(new { token });
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User"),
            };

            //Is this Secure?
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


    }
}
