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
using Org.BouncyCastle.Asn1.Ocsp;
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
    [Authorize(Roles = "User,Moderator,Admin")]
    public class UserController : BaseController
    {
        private Auth auth;

        public UserController(IConfiguration config, IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
            : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, userManager, roleManager, logger)
        {
            auth = new Auth(config);
        }
        
        // GET: api/User
        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsers()
        {
            var users = await UserManager.GetUsersInRoleAsync("User");

            return Ok(users) ;
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("UserRoles")]
        public async Task<ActionResult<IEnumerable<GetUserRoleDto>>> GetUserRoles ()
        {
            Logger.LogInformation("Retrieving the users with roles.");
            var users = await PlayerRepository.GetAll();
            IList<GetUserRoleDto> usersWithRoles = new List<GetUserRoleDto>();
            if(users.Count > 0)
            {
                foreach (var user in users)
                {
                    var roles = await UserManager.GetRolesAsync(user);
                    usersWithRoles.Add(new GetUserRoleDto
                    {
                        Id = Guid.Parse(user.Id),
                        Username = user.UserName,
                        Email = user.Email,
                        Role = roles,
                    });
                }
            }
            return Ok(usersWithRoles);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet("Roles")]
        public async Task<ActionResult> ExistingRoles()
        {
            Logger.LogInformation("Retrieving existing roles");
            var roles = RoleManager.Roles.ToArray();
            return Ok(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRole")]
        public async Task<ActionResult> UpdateRole(PutUserDto updateUser)
        {
            Logger.LogInformation("Trying to update the role of " + updateUser.Email);
            var user = PlayerRepository.TryFind(u => u.Email == updateUser.Email).Result.result;
            if (user == null) return NotFound("No matching user to update");

            Logger.LogInformation("Get the old and new role for the user");
            var oldRoles = await UserManager.GetRolesAsync(user);
            var newRole = await RoleManager.FindByNameAsync(updateUser.RoleName);

            Logger.LogInformation("Get the old claims for the user");
            var oldClaims = await UserManager.GetClaimsAsync(user);
            if (oldClaims != null)
            {
                Logger.LogInformation("Removing the old claims");
                await UserManager.RemoveClaimsAsync(user, oldClaims);
            }



            if (oldRoles != null && newRole != null && newRole.Name != null)
            {
                Logger.LogInformation("Removing the old role");
                await UserManager.RemoveFromRolesAsync(user, oldRoles);
                
                Logger.LogInformation("Adding the new role");
                await UserManager.AddToRoleAsync(user, newRole.Name);

                Logger.LogInformation("Adding the new claims");
                await UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
                await UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, newRole.Name));

                var updatedRole = await UserManager.GetRolesAsync(user);
                return Ok(updatedRole);
            }

            return BadRequest("Role was invalid");

        }



        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ResetPasswordToken(GetChangePasswordDto data)
        {
            Logger.LogInformation(data.Email + " forgot their password");

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

        [HttpPut("ForgotPassword")]

        public async Task<ActionResult> ResetPassword(PutForgotPasswordDto data)
        {
            Logger.LogInformation(data.Email + " wants to change their password");
            var user = await UserManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound("No user uses the entered email");

            var output = auth.ValidateToken(data.Token);
            if (output == null) return NotFound("Token invalid");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(data.Password);
            await UserManager.UpdateAsync(user);

            Logger.LogInformation(user.UserName + " changed their password");

            return Ok(user); ;
        }

        [HttpPut("ChangePassword")]

        public async Task<ActionResult> ChangePassword(PutChangePasswordDto data)
        {
            var user = await UserManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound("No user uses the entered email");

            if (!BCrypt.Net.BCrypt.Verify(data.OldPassword, user.PasswordHash))
            {
                return BadRequest(new { message = "Old password does not match" });
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);
            await UserManager.UpdateAsync(user);

            Logger.LogInformation(user.UserName + " changed their password");

            return Ok(user); ;
        }
    }
}
