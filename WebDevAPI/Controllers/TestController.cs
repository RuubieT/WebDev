using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDevAPI.Db.Dto_s.PokerTable;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic;
using WebDevAPI.Logic.CardLogic;

namespace WebDevAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : BaseController
    {
        private Auth auth;

        public TestController(IConfiguration config, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
                  IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
                  playerHandRepository, pokerTableRepository, logger)
        {
            auth = new Auth(config);
        }

        [HttpGet]
        public async Task<ActionResult> test()
        {
            var cards = await PlayerHandRepository.GetAll();
            auth.SendMailAsync("JOOOl").Wait();
            return Ok(cards);

        }

        [HttpGet("user")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await PlayerRepository.GetAll();
            return Ok(users);
        }


        [HttpGet("tablecards")]
        [Authorize(Policy = "Admin")]
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

        
        [HttpGet("cards")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult> GetCards()
        {
            var cards = await CardRepository.GetAll();
            return Ok(cards);
        }

        [HttpGet("pokertables")]
        public async Task<ActionResult> GetPokertables()
        {
            var pokertables = await PokerTableRepository.GetAll();
            return Ok(pokertables);
        }

        [HttpDelete("pokertable/{id}")]
        public async Task<ActionResult> DeletePokertable (Guid id)
        {
            var table = await PokerTableRepository.Get(id);
            await PokerTableRepository.Delete(table);
            return Ok("Deleted pokertable wtih id: " + id);
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
            return auth.CreateToken(players.First().Id, claims);
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
