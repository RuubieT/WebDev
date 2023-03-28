﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDevAPI.Db.Dto_s.PokerTable;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic;

namespace WebDevAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/test")]
    [ApiController]
    public class TestController : BaseController
    {
        private Auth auth;

        public TestController(IConfiguration config, IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
                  IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository) : base(contactFormRepository, userRepository, playerRepository, cardRepository,
                  playerHandRepository, pokerTableRepository)
        {
            auth = new Auth(config);
        }

        [HttpGet]
        public async Task<ActionResult> test()
        {
            var cards = await PlayerHandRepository.GetAll();

            return Ok(cards);

        }

        [HttpGet("user")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await PlayerRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("cards")]
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
            return auth.CreateToken(players.First(), claims);
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