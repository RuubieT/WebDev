using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Audit.Core;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using WebDevAPI.Db;
using WebDevAPI.Db.Dto_s.Contactform;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic;

namespace WebDevAPI.Controllers
{

    [Route("api/User")]
    [ApiController]
    //[Authorize]
    public class UserController : BaseController
    {
        private Auth auth;

        public UserController(IConfiguration config, IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager) : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, userManager, logger)
        {
            auth = new Auth(config);
        }
        
        // GET: api/User
       // [Authorize(Roles = "Admin, Moderator")]
        //[HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsers()
        {
            var users = await UserManager.GetUsersInRoleAsync("User");

            return Ok(users) ;
        }

        
        // DELETE: api/User/{email}
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await PlayerRepository.TryFind(u => u.Email == email);
            if (user.result == null) return NotFound("No user to delete");
            await PlayerRepository.Delete(user.result);

            return NoContent();
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ResetPasswordToken(GetChangePasswordDto data)
        {
            var user = await UserManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound("No user uses the entered email");
            List<Claim> claims = new List<Claim>()
            {
                new Claim("User", "User"),
            };
            Logger.LogInformation("Unique code sent");
            string token = auth.CreateToken(user.Id, claims);
            auth.SendMailAsync(token.ToString()).Wait();

            Logger.LogInformation("Email send to " + data.Email);

            
            
            return Ok(new { token });
        }

        [HttpPut("ChangePassword")]

        public async Task<ActionResult> ResetPassword(PostChangePasswordDto data)
        {
            var user = await UserManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound("No user uses the entered email");

            var output = auth.ValidateToken(data.Token);
            if (output == null) return NotFound("Token invalid");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(data.Password);
            await UserManager.UpdateAsync(user);

            Logger.LogInformation(user.UserName + " changed their password");

            return Ok(user); ;
        }
    }
}
