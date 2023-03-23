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
    [Authorize(Roles = "User")]
    [Route("api/User")]
    [ApiController]
    public class UserController : BaseController
    {

        public UserController(IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository)
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

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUser(Guid id)
        {
            var users = await UserRepository.GetAll();
          if (users == null)
          {
              return NotFound();
          }
            var user = await UserRepository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user.GetUserDto();
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<GetUserDto>> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            var getUser = await UserRepository.Get(id);

            if (getUser == null)
            {
                return NotFound();
            }

            await UserRepository.Update(getUser);

             return getUser.GetUserDto();
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

        private bool UserExists(Guid id)
        {
            return (UserRepository.TryFind(e => e.Id == id)).Result.succes;
        }
    }
}
