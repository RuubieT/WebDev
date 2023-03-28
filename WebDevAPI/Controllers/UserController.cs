using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevAPI.Db;
using WebDevAPI.Db.Dto_s.Contactform;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/User")]
    [ApiController]
    public class UserController : BaseController
    {

        public UserController(IConfiguration config, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository,
                IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, pokerTableRepository)
        {

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
    }
}
