using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDevAPI.Db.Dto_s.PokerTable;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic;

namespace WebDevAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/test")]
    [ApiController]
    public class TestController : BaseController
    {
        private Auth auth;

        public TestController(IConfiguration config, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository)
        {
            auth = new Auth(config);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPokerTableDto>>> GetUsers()
        {
            var users = await PokerTableRepository.GetAll();
            var getUsers = new List<GetPokerTableDto>();
            if (users == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var user in users)
                {
                    getUsers.Add(user.GetPokerTableDto());
                }
            }
            return Ok(getUsers);
        }

        [HttpGet("playerhands")]
        public async Task<ActionResult<IEnumerable<GetPokerTableDto>>> GetHands()
        {
            var hands = await PlayerHandRepository.GetAll();
            
            return Ok(hands);
        }

        [HttpGet("token")]
        public async Task<ActionResult<string>> GetToken()
        {
            var players = await PlayerRepository.GetAll();
            if (players == null)
            {
                return NotFound();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, players.First().Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };
            return auth.CreateToken(players.First(), claims);
        }

        [HttpPost("pokertable")]
        public async Task<ActionResult<GetPokerTableDto>> CreatePokerTable(PostLoginUserDto request)
        {
            PokerTable pokerTable = new PokerTable
            {
                PokerTableId = new Guid(),
                Ante = 10,
                SmallBlind = 20,
                BigBlind = 30,
                MaxSeats = 8,
            };
            await PokerTableRepository.Create(pokerTable);

            return pokerTable.GetPokerTableDto();
        }


    }
}
