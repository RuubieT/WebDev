using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic;

namespace WebDevAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private Auth auth;

        public AuthController(IConfiguration config, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository)
        {

            auth = new Auth(config);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(PostPlayerDto request)
        {
            var requestedUser = await UserRepository.TryFind(e => e.Email == request.Email);
            if (requestedUser.succes) return BadRequest("User already exists");

            var userNameExists = await PlayerRepository.TryFind(e => e.Username == request.Username);
            if (userNameExists.succes) return BadRequest("Username is already taken");

            var player = new Player
            {
                Id = new Guid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                PokerTableId = PokerTableRepository.GetAll().Result[0].PokerTableId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await PlayerRepository.Create(player);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "User"),
            };
            string token = auth.CreateToken(player, claims);

            return Ok(new { token });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(PostLoginUserDto request)
        {
            var allplayers = await PlayerRepository.GetAll();

            var data = await PlayerRepository.TryFind(e => e.Email.ToLower() == request.Email.ToLower());
            var findPlayer = data.result;
            if (findPlayer == null) return NotFound();


            if (findPlayer.Email != request.Email)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, findPlayer.PasswordHash))
            {
                return BadRequest("Email or password is wrong.");
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, findPlayer.Username),
                new Claim(ClaimTypes.Role, "User"),
            };
            string token = auth.CreateToken(findPlayer, claims);

            return Ok(new { token });
        }

    }
}
