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
using Google.Authenticator;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Azure.Core;

namespace WebDevAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : BaseController
    {
        private Auth auth;
        private readonly RoleManager<IdentityRole> _roleManager;


        public TestController(IConfiguration config, IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager) : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, logger, userManager)
        {
            auth = new Auth(config);
            _roleManager = roleManager;
 
        }

        [HttpGet]
        public async Task<ActionResult> test()
        {
            var data = await PlayerRepository.TryFindAll(p => p.UserName == "___TROLL___");
            //var data = await PlayerRepository.GetAll();
            return Ok(data);

        }

        [HttpGet("roles")]
        public async Task<ActionResult> addRoles()
        {
            string[] roleNames = { "Admin", "Moderator", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            return Ok();

        }

        [HttpGet("createAdminAndMod")]
        public async Task<ActionResult> CreateAdmin()
        {
            var Admin = new IdentityUser
            {
                UserName = "Admin",
                Email = "Admin@Admin"
            };
            var Moderator = new IdentityUser
            {
                UserName = "Moderator",
                Email = "Moderator@Moderator"
            };
            var suc = await UserManager.CreateAsync(Admin, BCrypt.Net.BCrypt.HashPassword("Admin"));
            var ceed = await UserManager.CreateAsync(Moderator, BCrypt.Net.BCrypt.HashPassword("Moderator"));
                if( suc.Succeeded && ceed.Succeeded)
            {
                await UserManager.AddToRoleAsync(Admin, "Admin");
                await UserManager.AddToRoleAsync(Moderator, "Moderator");
            }
            return Ok();

        }

        [HttpGet("user")]
        public async Task<ActionResult> GetUsers()
        {
            var user = new IdentityUser
            {
                UserName = "LemmeKNow",
                Email = "TEst",
            };

            var result = await UserManager.CreateAsync(user, BCrypt.Net.BCrypt.HashPassword("Working"));

            if (result.Succeeded)
            {
                await UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                await UserManager.AddToRoleAsync(user, "User");
            }


            var player = new Player
            {
                Id = await UserManager.GetUserIdAsync(user),
                FirstName = "BOB",
                LastName = "BOB",
                UserName = user.UserName,
                Email = user.Email,
                Chips = 1500,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Working"),
            };

            await PlayerRepository.Create(player);
            Console.WriteLine("CREATED");

            var getplayer = PlayerRepository.Get(Guid.Parse(player.Id));
            if (getplayer == null)
            {
                Console.WriteLine("HERE");
            }

            var users = await UserManager.GetUsersInRoleAsync("User");
            return Ok(users);
        }


        [HttpGet("tablecards")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "User")]
        public async Task<ActionResult> GetCards()
        {
            var user = HttpContext.User;
            var cards = await CardRepository.GetAll();
            return Ok(cards);
        }


    }
}
