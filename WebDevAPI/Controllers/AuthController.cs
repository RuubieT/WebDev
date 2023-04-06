using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;

        public AuthController(IConfiguration config, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, logger)
        {
            auth = new Auth(config);
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(PostPlayerDto request)
        {
            var requestedUser = await UserRepository.TryFind(e => e.Email == request.Email);
            if (requestedUser.succes) return BadRequest("User already exists");

            var userNameExists = await PlayerRepository.TryFind(e => e.Username == request.Username);
            if (userNameExists.succes) return BadRequest("Username is already taken");

            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(user, BCrypt.Net.BCrypt.HashPassword(request.Password));

            if (result.Succeeded)
            {
                Logger.LogInformation(request.FirstName + " created a new account with password.");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                Logger.LogInformation(request.FirstName + " was given the role of a user");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            var player = new Player
            {
                Id = Guid.Parse(await _userManager.GetUserIdAsync(user)),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await PlayerRepository.Create(player);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "User"),
            };
            string token = auth.CreateToken(player.Id, claims);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true
            });

            Logger.LogInformation("Registered user: " +  request.Username);

            return Ok(new { token });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(PostLoginUserDto request)
        {
            var data = await PlayerRepository.TryFind(e => e.Email.ToLower() == request.Email.ToLower());
            var findPlayer = data.result;
            if (findPlayer == null) return NotFound();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return NotFound();

            if (!BCrypt.Net.BCrypt.Verify(request.Password, findPlayer.PasswordHash))
            {
                return BadRequest(new {message = "Invalid Credentials"});
            }
            
            

            var result = await _signInManager.CanSignInAsync(user);
            if (result)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                Logger.LogInformation("User logged in.");
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, findPlayer.Username),
                new Claim(ClaimTypes.Role, "User"),
            };
            string token = auth.CreateToken(findPlayer.Id, claims);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true
            });

            Logger.LogInformation("User: " + findPlayer.Username + " logged in");

            return Ok(new { token });
        }

        [HttpGet("User")]
        public async Task<ActionResult<GetPlayerDto>> GetUser()
        {
            var token = Request.Cookies["jwt"];
            if(token == null) return NotFound();
            var jwt = auth.ValidateToken(token);

            Guid userId = Guid.Parse(jwt.Issuer);

            var player = await PlayerRepository.TryFind(user => user.Id == userId);

            if (!player.succes || player.result == null) return BadRequest("User not found");

            return Ok(player.result.GetPlayerDto());

        }

        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Success" });
        }
    }
}
