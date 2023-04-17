using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.PlayerHand;
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
        DealCards DealCards;

        public PokerTableController(IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, logger, userManager)
        {
            DealCards = new DealCards();
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

        [HttpPut("Join")]
        public async Task<ActionResult<IEnumerable<GetPlayerDto>>> JoinGame(PutJoinPokerTableDto data)
        {
            var user = await UserManager.FindByNameAsync(data.Username);
            if (user == null) return NotFound("No matching player found");
            var result = await PlayerRepository.Get(Guid.Parse(user.Id));
            if (result.PokerTableId != null) return BadRequest("Already in a game");

            var pokertable = await PokerTableRepository.Get(data.PokerTableId);
            if (pokertable == null) return NotFound("No matching pokertable found");
            result.PokerTableId = data.PokerTableId;

            await PlayerRepository.Update(result);

            return Ok(result.GetPlayerDto());
        }

        [HttpGet("{pokertableId}")]
        public async Task<ActionResult<GetPokerTableDto>> GetPokertable(Guid pokertableId)
        {
            var pokertable = await PokerTableRepository.Get(pokertableId);
            if (pokertable == null) return NotFound("No pokertable found!");

            return Ok(pokertable.GetPokerTableDto());
        }

        [HttpGet("Players/{pokertableId}")]
        public async Task<ActionResult<ICollection<Player>>> GetPlayers(Guid pokertableId)
        {
            var pokertable = await PokerTableRepository.Get(pokertableId);
            if (pokertable == null) return NotFound("No pokertable found!");

            var players = await PlayerRepository.TryFindAll(p => p.PokerTableId == pokertableId);
            if (players == null || !(players.Count > 0)) return BadRequest("No players on the table");
            var playersDto = new List<GetPlayerDto>();
            foreach (var player in players)
            {
                playersDto.Add(player.GetPlayerDto());
            }

            return Ok(playersDto);
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

            
            List<PlayerHand> playerHands = (List<PlayerHand>)DealCards.Deal(players, deck);
            foreach (var hand in playerHands)
            {
                await PlayerHandRepository.Create(hand);
                var card1 = hand.FirstCard;
                card1.InHand = true;
                await CardRepository.Update(card1);
                var card2 = hand.SecondCard;
                card2.InHand = true;
                await CardRepository.Update(card2);
            }
            return Ok(pokertable.GetPokerTableDto());
        }

        [HttpGet("Hand/{username}")]
        public async Task<ActionResult<GetPlayerHandDto>> GetHand(string username)
        {
            var data = await UserManager.FindByNameAsync(username);
                await PlayerRepository.TryFind(u => u.Username == username);
            if (data == null) return NotFound();

            var playerhand = await PlayerHandRepository.TryFind(h => h.PlayerId == Guid.Parse(data.Id));
            if (playerhand.result == null || playerhand.result.FirstCardId == null || playerhand.result.SecondCardId == null) return NotFound("No hand found");

            var firstcard = await CardRepository.Get(playerhand.result.FirstCardId ?? Guid.Empty);
            var secondcard = await CardRepository.Get(playerhand.result.SecondCardId ?? Guid.Empty);
            if (firstcard == null || secondcard == null) return NotFound("No cards found");
            return Ok(new PlayerHand
            {
                FirstCard = firstcard,
                SecondCard = secondcard,
            }.GetPlayerHandDto());

        }
    }
}
