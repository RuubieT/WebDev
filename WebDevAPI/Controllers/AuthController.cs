using Azure.Core;
using Google.Authenticator;
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
using WebDevAPI.Db.Repositories;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WebDevAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private Auth auth;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(IConfiguration config, IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, logger, userManager)
        {
            auth = new Auth(config);
            _signInManager = signInManager;
            }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(PostPlayerDto request)
        {
            var requestedUser = await UserManager.FindByEmailAsync(request.Email);
            if (requestedUser != null) return BadRequest("User already exists");

            var userNameExists = await UserManager.FindByNameAsync(request.Username);
            if (userNameExists != null) return BadRequest("Username is already taken");

            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,
            };

            var result = await UserManager.CreateAsync(user, BCrypt.Net.BCrypt.HashPassword(request.Password));

            if (result.Succeeded)
            {
                Logger.LogInformation(request.FirstName + " created a new account with password.");
                await UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                await UserManager.AddToRoleAsync(user, "User");
                Logger.LogInformation(request.FirstName + " was given the role of a user");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            string key = auth.GenerateRandomString(10);

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode("S1144640 Web app", request.Email, key, false, 3);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string manualEntrySetupCode = setupInfo.ManualEntryKey;


            var player = new Player
            {
                Id = await UserManager.GetUserIdAsync(user),
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                AuthCode = key,
            };
            try
            {
                await PlayerRepository.IdentityUserToPlayer(user, player, UserManager);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

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

            return Ok(new { token, qrCodeImageUrl, manualEntrySetupCode });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(PostLoginUserDto request)
        {
            var user = await UserManager.FindByEmailAsync(request.Email);
            if (user == null) return NotFound();

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest(new {message = "Invalid Credentials"});
            }

            var result = await _signInManager.CanSignInAsync(user);
            if (result)
            {
                Logger.LogInformation("User logged in.");
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User"),
            };
            string token = auth.CreateToken(user.Id, claims);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true
            });

            Logger.LogInformation("User: " + user.UserName + " logged in");

            return Ok(new { token });
        }

        [HttpGet("User")]
        public async Task<ActionResult<GetPlayerDto>> GetUser()
        {
            var token = Request.Cookies["jwt"];
            if(token == null) return NotFound();
            var jwt = auth.ValidateToken(token);

            string userId = jwt.Issuer;

            var player = await UserManager.FindByIdAsync(userId);

            if (player == null) return BadRequest("User not found");

            return Ok(player);

        }

        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Success" });
        }

        [HttpPost("GAuth")]
        public async Task<ActionResult> ValidateCode(PostGAuthCodeDto data)
        {
            var user = await UserManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound();

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            //bool result = tfa.ValidateTwoFactorPIN(user.AuthCode, data.Code);
            //if (!result) return BadRequest("Invalid code");
            return Ok(new { message = "Success" });
        }
    }
}
