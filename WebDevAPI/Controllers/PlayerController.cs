using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Core;
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
using static Duende.IdentityServer.Models.IdentityResources;

namespace WebDevAPI.Controllers
{
    [Route("api/Player")]
    [ApiController]
    [Authorize(Roles ="User,Moderator,Admin")]
    public class PlayerController : BaseController
    {

        public PlayerController(IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
            : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, userManager, roleManager, logger)
        {

        }

        // GET: api/Player
        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlayerDto>>> GetPlayers()
        {
            var Players = await PlayerRepository.GetAll();
            var getPlayers = new List<GetPlayerDto>();
            if (Players == null)
            {
                return NotFound("No players");
            } else {
               foreach (var Player in Players)
                {
                    getPlayers.Add(Player.GetPlayerDto());
                }
            }
            return Ok(getPlayers) ;
        }

        // DELETE: api/Player/{email}
        [Authorize(Roles = "Moderator,Admin")]
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = PlayerRepository.TryFind(u => u.Email == email).Result.result;
            if (user == null) return NotFound("No user to delete");

            //Make sure no loose references are lingering around
            if(user.PokerTableId != null)
            {
                var hand = PlayerHandRepository.TryFind(h => h.PlayerId == user.Id).Result.result;
                if(hand != null)
                {
                    await PlayerHandRepository.Delete(hand);
                }
                
                user.PokerTableId = null;
            }

            await PlayerRepository.Delete(user);

            return NoContent();
        }

        // GET: api/player/Find/{email}
        [HttpGet("Find/{email}")]
        public async Task<ActionResult<GetPlayerDto>> GetPlayer(string email)
        {
            var data = await PlayerRepository.TryFind(u => u.Email == email);
            if(data.result == null) return NotFound("Not user uses the given email");

            Logger.LogInformation("Retrieved user by email: " + email);

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
                return NotFound("Not players");
            }
            else
            {
                foreach (var Player in Players)
                {
                    getPlayers.Add(Player.GetLeaderBoardDto());
                }
            }
            Logger.LogInformation("Leaderboard loaded in");
            return Ok(getPlayers.OrderByDescending(p => p.Chips));
        }
    }
}
