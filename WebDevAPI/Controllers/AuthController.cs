using Audit.Core;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager) : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, userManager, roleManager, logger)
        {
            auth = new Auth(config);
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(PostPlayerDto request)
        {
            Logger.LogInformation(request.Email + " is trying to register");
            var requestedUser = await UserManager.FindByEmailAsync(request.Email);
            if (requestedUser != null) return BadRequest("User already exists");

            var userNameExists = await UserManager.FindByNameAsync(request.Username);
            if (userNameExists != null) return BadRequest("Username is already taken");

            string key = auth.GenerateRandomString(10);

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode("S1144640 Web app", request.Email, key, false, 3);
            Logger.LogInformation("Two Factory Authentication is setupped");

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string manualEntrySetupCode = setupInfo.ManualEntryKey;

            var player = new Player
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                AuthCode = key,
            };
            try
            {
                using (AuditFactory.Create("User:Create",()=> player))
                {
                    await PlayerRepository.Create(player);
                }
               
       
                await UserManager.AddClaimAsync(player, new Claim(ClaimTypes.Role, "User"));
                await UserManager.AddToRoleAsync(player, "User");
                Logger.LogInformation(request.Username + " was given the role of a user");
                AuditFactory.Log("Create User", new { ExtraField = (request.Username + " was given the role of a user")});
                await _signInManager.SignInAsync(player, isPersistent: false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }    

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "User"),
            };
            string token = auth.CreateToken(player.Id, claims);
            Logger.LogInformation("Creating a jwt token for " + request.Username);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });
            Logger.LogInformation("JWT token appended");
            Logger.LogInformation("Registered user: " +  request.Username);

            return Ok(new { token, qrCodeImageUrl, manualEntrySetupCode });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(PostLoginUserDto request)
        {
            Logger.LogInformation(request.Email + " is trying to login");
            var user = PlayerRepository.TryFind(u => u.Email == request.Email).Result.result;
            if (user == null) return NotFound("No user found");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Invalid Credentials");
            }

            var result = false;
            using (AuditFactory.Create("User:LoggedIn", () => user))
            {
                result = await _signInManager.CanSignInAsync(user);
            }
            
            if (result)
            {
                    Logger.LogInformation("User logged in.");
            }

            var claims = await UserManager.GetClaimsAsync(user);
            string token = auth.CreateToken(user.Id, (List<Claim>)claims);

            Logger.LogInformation("Creating a jwt token for " + user.UserName);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });
            Logger.LogInformation("JWT token appended");
            Logger.LogInformation("Logged in user: " + user.UserName);
            Logger.LogInformation("User: " + user.UserName + " logged in");

            return Ok(new { token });
        }

 
        [HttpGet("User")]
        public async Task<ActionResult<GetPlayerDto>> GetUser()
        {
            var token = Request.Cookies["jwt"];
            if(token == null) return Ok("No logged in user");
            var jwt = auth.ValidateToken(token);
            Logger.LogInformation("Validating token");

            string userId = jwt.Issuer;

            var player = await UserManager.FindByIdAsync(userId);

            if (player == null) return NotFound("User not found");

            Logger.LogInformation(player + " data retrieved");

            var role = "";
            foreach(var claim in jwt.Claims)
            {
                if(claim.Type == ClaimTypes.Role)
                {
                    role = claim.Value;
                }
            }

            return Ok(new { player, role });

        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            Logger.LogInformation("User logged out");

            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Success" });
        }

        [HttpPost("GAuth")]
        public async Task<ActionResult> ValidateCode(PostGAuthCodeDto data)
        {

            Logger.LogInformation(data.Email + " wants to verify their 2FA code!");

            var user = PlayerRepository.TryFind(u => u.Email == data.Email).Result.result;
            if (user == null) return NotFound("User not found");
            var player = await PlayerRepository.GetByString(user.Id);

            Logger.LogInformation(player + " is an existing user");



            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            //#TODO temporarily turned off for testing purposes
            //bool result = tfa.ValidateTwoFactorPIN(player.AuthCode, data.Code);
            //if (!result) return BadRequest("Invalid code");

            Logger.LogInformation("Code matches the 2FA");

            return Ok(new { message = "Success" });
        }
    }
}
