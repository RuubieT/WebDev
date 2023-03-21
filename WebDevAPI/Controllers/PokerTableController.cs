using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic.CardLogic;

namespace WebDevAPI.Controllers
{
    [Route("api/PokerTable")]
    [ApiController]
    public class PokerTableController : BaseController
    {
        public PokerTableController(IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository)
        {

        }

   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlayerDto>>> GetPlayers()
        {
            var players = await PlayerRepository.GetAll();
            var getPlayers = new List<GetPlayerDto>();
            if (players == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var p in players)
                {
                    getPlayers.Add(p.GetPlayerDto());
                }
            }
            return Ok(getPlayers);

        }

        [HttpGet("Create")]
        public async Task<ActionResult<string>> CreateGame()
        {
            var deck = new DeckOfCards();
            deck.SetUpDeck();
            return Ok(deck.getDeck);
        }

        [HttpGet("Join")]
        public async Task<ActionResult<string>> JoinGame()
        {
            return Ok("Join a game");
        }
    }
}
