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
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : BaseController
    {

        public PlayerController(IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, IUserRepository userRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository)
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

        // GET: api/Player/Leaderboard
        [HttpGet("Leaderboard"), AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetLeaderBoardDto>>> GetLeaderboard()
        {
            Console.WriteLine(this.HttpContext.User);
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
