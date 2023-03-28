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
        public PokerTableController(IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
             IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
             playerHandRepository, pokerTableRepository)
        {

        }

        [HttpPost("Create")]
        public async Task<ActionResult<IEnumerable<GetPokerTableDto>>> CreateGame(PostCreatePokerTableDto creator)
        {
            var result = PlayerRepository.TryFind(u => u.Username == creator.Username).Result;
            if (result.result == null || !result.succes) return NotFound("No matching player found");
            if (result.result.PokerTableId != null) return BadRequest("Already in a game");

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

            return Ok(pokerTable.GetPokerTableDto());
        }

        [HttpGet("Join")]
        public async Task<ActionResult<string>> JoinGame()
        {
            return Ok("Join a game");
        }

        [HttpGet("Start/{pokertableId}")]
        public async Task<ActionResult<GetPokerTableDto>> StartGame(Guid pokertableId)
        {
            var cards = await CardRepository.GetAll();
            var deck = new Queue<Card>((IEnumerable<Card>)cards);

            var pokertable = await PokerTableRepository.Get(pokertableId);
            if (pokertable == null) return NotFound("No pokertable found!");

            var players = await PlayerRepository.TryFindAll(p => p.PokerTableId == pokertableId);
            if (players == null || !(players.Count > 0)) return BadRequest("No players on the table");

            DealCards dealCards = new DealCards();
            List<PlayerHand> playerHands = (List<PlayerHand>)dealCards.Deal(players, deck);
            foreach (var hand in playerHands)
            {
                await PlayerHandRepository.Create(hand);
            }
            return Ok(pokertable.GetPokerTableDto());
        }
    }
}
