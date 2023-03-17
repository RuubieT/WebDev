using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{
    [Route("api/PokerTable")]
    [ApiController]
    public class PokerTableController : BaseController
    {
        public PokerTableController(IUserRepository userRepository, IContactFormRepository contactFormRepository)
            : base(userRepository, contactFormRepository)
        {
        }

        [HttpGet("Create")]
        public async Task<ActionResult<string>> CreateGame()
        {
            return Ok("Create a game");
        }

        [HttpGet("Join")]
        public async Task<ActionResult<string>> JoinGame()
        {
            return Ok("Join a game");
        }
    }
}
