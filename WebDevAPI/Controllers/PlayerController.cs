using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: api/player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPlayerDto>> GetPlayer(Guid id)
        {
            var Players = await PlayerRepository.GetAll();
          if (Players == null)
          {
              return NotFound();
          }
            var Player = await PlayerRepository.Get(id);

            if (Player == null)
            {
                return NotFound();
            }

            return Player.GetPlayerDto();
        }

        // PUT: api/player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<GetPlayerDto>> PutPlayer(Guid id, Player Player)
        {
            if (id != Player.Id)
            {
                return BadRequest();
            }
            var getPlayer = await PlayerRepository.Get(id);

            if (getPlayer == null)
            {
                return NotFound();
            }

            await PlayerRepository.Update(getPlayer);

             return getPlayer.GetPlayerDto();
        }

        // POST: api/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetPlayerDto>> PostPlayer(Player Player)
        {
          if (PlayerRepository.GetAll() == null)
          {
              return Problem("Entity set 'WebDevDbContext.Players'  is null.");
          }
            await PlayerRepository.Create(Player);

            return Player.GetPlayerDto();
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var Player = await PlayerRepository.Get(id);
            if (Player == null)
            {
                return NotFound();
            }
            await PlayerRepository.Delete(Player);

            return NoContent();
        }

        private bool PlayerExists(Guid id)
        {
            return (PlayerRepository.TryFind(e => e.Id == id)).Result.succes;
        }
    }
}
