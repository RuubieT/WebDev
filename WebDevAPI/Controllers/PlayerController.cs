using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevAPI.Db;
using WebDevAPI.Db.Dto_s.Contactform;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{
    [Route("api/Player")]
    [ApiController]
    //[Authorize(Roles ="User")]
    public class PlayerController : BaseController
    {

        public PlayerController(IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager) : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, logger, userManager)
        {

        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlayerDto>>> GetPlayers()
        {
            var Players = await PlayerRepository.GetAll();
            var getPlayers = new List<GetPlayerDto>();
            if (Players == null)
            {
                return NotFound();
            } else {
               foreach (var Player in Players)
                {
                    getPlayers.Add(Player.GetPlayerDto());
                }
            }
            return Ok(getPlayers) ;
        }

        // GET: api/player/Find/{email}
        [HttpGet("Find/{email}")]
        public async Task<ActionResult<GetPlayerDto>> GetPlayer(string email)
        {
            var data = await PlayerRepository.TryFind(u => u.Email == email);
            if(data.result == null) return NotFound();

            return data.result.GetPlayerDto();
        }

        // GET: api/Player/Leaderboard
        [HttpGet("Leaderboard"), AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetLeaderBoardDto>>> GetLeaderboard()
        {
            var Players = await PlayerRepository.GetAll();
            var getPlayers = new List<GetLeaderBoardDto>();
            if (Players == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var Player in Players)
                {
                    getPlayers.Add(Player.GetLeaderBoardDto());
                }
            }
            return Ok(getPlayers.OrderByDescending(p => p.Chips));
        }
    }
}
