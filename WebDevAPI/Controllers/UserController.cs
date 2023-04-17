using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class UserController : BaseController
    {
        private Auth auth;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IConfiguration config, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
                IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, UserManager<IdentityUser> userManager, ILogger<BaseController> logger) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
                playerHandRepository, pokerTableRepository, logger)
        {
            auth = new Auth(config);
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsers()
        {
            var users = await UserRepository.GetAll();
            var getUsers = new List<GetUserDto>();
            if (users == null)
            {
                return NotFound();
            } else {
               foreach (var user in users)
                {
                    getUsers.Add(user.GetUserDto());
                }
            }
            return Ok(getUsers) ;
        }


        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await UserRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            await UserRepository.Delete(user);

            return NoContent();
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ResetPasswordToken(GetChangePasswordDto data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound();
            List<Claim> claims = new List<Claim>()
            {
                new Claim("User", "User"),
            };
            string token = auth.CreateToken(new Guid(user.Id), claims);
            auth.SendMailAsync(token.ToString()).Wait();

            Logger.LogInformation("Email send to " + data.Email);

            
            
            return Ok(new { token });
        }

        [HttpPut("ChangePassword")]

        public async Task<ActionResult> ResetPassword(PostChangePasswordDto data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound();

            var output = auth.ValidateToken(data.Token);
            if (output == null) return NotFound("Token invalid");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(data.Password);
            await _userManager.UpdateAsync(user);

            return Ok(user); ;
        }
    }
}
