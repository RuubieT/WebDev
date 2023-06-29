using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.PlayerHand;
using WebDevAPI.Db.Dto_s.PokerTable;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic.CardLogic;
using static Duende.IdentityServer.Models.IdentityResources;


namespace WebDevAPI.Controllers
{
    [Authorize(Roles = "User,Moderator,Admin")]
    [Route("api/Pokertable")]
    [ApiController]
    public class PokerTableController : BaseController
    {
        DealCards DealCards;

        public PokerTableController(IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
            : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, userManager, roleManager, logger)
        {
            DealCards = new DealCards();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<IEnumerable<GetPokerTableDto>>> CreateGame(PostCreatePokerTableDto creator)
        {
            var user = PlayerRepository.TryFind(u => creator.Username == u.UserName).Result.result;
            if (user == null) return NotFound("No matching player found");
            var player = await PlayerRepository.GetByString(user.Id);
            if (player.PokerTableId != null) return BadRequest("Already in a game");

            Logger.LogInformation("Creating a new pokertable for user: " + creator.Username);

            PokerTable pokerTable = new PokerTable
            {
                PokerTableId = Guid.NewGuid(),
                Ante = 10,
                SmallBlind = 20,
                BigBlind = 40,
                MaxSeats = 8
            };
            //using (AuditScopeFactory.Create("Pokertable:Create", () => pokerTable))
            //{
                await PokerTableRepository.Create(pokerTable);
            //}
            player.PokerTableId = pokerTable.PokerTableId;
            //using (AuditScopeFactory.Create("Player:Update", () => player))
            //{
                await PlayerRepository.Update(player);
            //}
            Logger.LogInformation("Adding the player to the table");
            Logger.LogInformation("Pokertable created with id " + pokerTable.PokerTableId);
            return Ok(pokerTable.GetPokerTableDto());
        }

        [HttpPut("Join")]
        public async Task<ActionResult<IEnumerable<GetPlayerDto>>> JoinGame(PutJoinPokerTableDto data)
        {
            var user = PlayerRepository.TryFind(u => data.Username == u.UserName).Result.result;
            if (user == null) return NotFound("No matching player found");
            var result = await PlayerRepository.GetByString(user.Id);
            if (result.PokerTableId != null) return BadRequest("Already in a game");

            var pokertable = await PokerTableRepository.Get(data.PokerTableId);
            if (pokertable == null) return NotFound("No matching pokertable found");
            result.PokerTableId = data.PokerTableId;

            Logger.LogInformation("Adding the player to the table");

            await PlayerRepository.Update(result);
            
            Logger.LogInformation(data.Username + " Added to pokertable: " + pokertable.PokerTableId);

            return Ok(result.GetPlayerDto());
        }

        //[Authorize(Roles = "Admin,Moderator")]
        [HttpGet("Start/{pokertableId}")]
        public async Task<ActionResult<GetPokerTableDto>> StartGame(Guid pokertableId)
        {
            var cards = await CardRepository.GetAll();
            var deckOfCards = new DeckOfCards();
            deckOfCards.ShuffleCards(cards);
            var deck = new Queue<Card>(deckOfCards.getDeck);

            Logger.LogInformation("New deck shuffled");

            var pokertable = await PokerTableRepository.Get(pokertableId);
            if (pokertable == null) return NotFound("No pokertable found!");

            var players = await PlayerRepository.TryFindAll(p => p.PokerTableId == pokertableId);
            if (players == null || !(players.Count > 0)) return BadRequest("No players on the table");

            Logger.LogInformation("Resetting the player their hands");
            await ClearHands(players);

            
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
                //From this moment the game is started
            }
            Logger.LogInformation("Hands dealt");

            var cardsLeft = await CardRepository.TryFindAll(c => c.InHand == false);
            deckOfCards.EmptyDeck();
            deckOfCards.ShuffleCards(cardsLeft);
            var deckLeft = new Queue<Card>(deckOfCards.getDeck);
            var tablecards = DealCards.TableCards(deckLeft);
            Logger.LogInformation("Tablecards drawn");

            return Ok(tablecards);
        }

        [HttpGet("tablecards")]
        public async Task<ActionResult> GetTableCards()
        {
            var cards = await CardRepository.TryFindAll(c => c.InHand == false);
            var deck = new Queue<Card>((IEnumerable<Card>)cards);
            DealCards d = new DealCards();
            DeckOfCards deck2 = new DeckOfCards();
            deck2.SetUpDeck();
            var tablecards = d.TableCards(deck2.getDeck);

            return Ok(tablecards);
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

            Logger.LogInformation("Retrieve players from table " + pokertableId);

            return Ok(playersDto);
        }

        [HttpGet("Hand/{username}")]
        public async Task<ActionResult<GetPlayerHandDto>> GetHand(string username)
        {
            var player = await PlayerRepository.TryFind(p=> p.UserName == username);
            if (player.result == null) return NotFound("No matching player found");


            var playerhand = await PlayerHandRepository.TryFind(h => h.PlayerId == player.result.Id);
            if (playerhand.result == null || playerhand.result.FirstCardId == null || playerhand.result.SecondCardId == null) return NotFound("No hand found");

            var firstcard = await CardRepository.Get(playerhand.result.FirstCardId ?? Guid.Empty);
            var secondcard = await CardRepository.Get(playerhand.result.SecondCardId ?? Guid.Empty);
            if (firstcard == null || secondcard == null) return NotFound("No cards found");

            Logger.LogInformation("Retrieve hand from " + username);

            return Ok(new PlayerHand
            {
                FirstCard = firstcard,
                SecondCard = secondcard,
            }.GetPlayerHandDto());

        }


        [HttpGet("{pokertableId}")]
        public async Task<ActionResult<GetPokerTableDto>> GetPokertable(Guid pokertableId)
        {
            var pokertable = await PokerTableRepository.Get(pokertableId);
            if (pokertable == null) return NotFound("No pokertable found!");

            return Ok(pokertable.GetPokerTableDto());
        }

        private async Task ClearHands(IList<Player> players)
        {
            var inHandCards = await CardRepository.TryFindAll(c => c.InHand == true);
            if (inHandCards != null && inHandCards.Count > 0)
            {
                foreach (var card in inHandCards)
                {
                    card.InHand = false;
                    await CardRepository.Update(card);
                }
            }
            
            //Remove existing hands
            foreach (var player in players)
            {
                var hand = await PlayerHandRepository.TryFind(h => h.PlayerId == player.Id);

                if (hand.result != null)
                {
                    await PlayerHandRepository.Delete(hand.result);
                }
            }
        }
    }
}
