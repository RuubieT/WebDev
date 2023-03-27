using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.PokerTable;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic.CardLogic;

namespace WebDevAPI.Controllers
{
    [Route("api/Pokertable")]
    [ApiController]
    public class PokerTableController : BaseController
    {
        public PokerTableController(IConfiguration config, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository,
                IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, pokerTableRepository)
        {

        }

        [HttpPost("Create")]
        public async Task<ActionResult<IEnumerable<GetPokerTableDto>>> CreateGame(PostCreatePokerTableDto creator)
        {

            var result = PlayerRepository.TryFind(e => e.Email == creator.Email).Result;
            var check = PlayerRepository.TryFind(u => u.Username == creator.Username).Result;

            if (result.result == null || check.result == null || !result.succes || !check.succes) return NotFound("No matching player found");


           var deck = new DeckOfCards();
           deck.SetUpDeck();

            ICollection<Player> players = new List<Player>
            {
                result.result
            };

            PokerTable pokerTable = new PokerTable
            {
                PokerTableId = new Guid(),
                Ante = 10,
                SmallBlind = 20,
                BigBlind = 30,
                MaxSeats = 8,
                Players = players
            };

            await PokerTableRepository.Create(pokerTable);

            DealCards dealCards = new DealCards();
            List<PlayerHand> playerHands = (List<PlayerHand>)dealCards.Deal(players, deck);
            

            return Ok(pokerTable.GetPokerTableDto());
        }

        [HttpGet("Join")]
        public async Task<ActionResult<string>> JoinGame()
        {
            return Ok("Join a game");
        }
    }
}
