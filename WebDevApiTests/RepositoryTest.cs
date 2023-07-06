using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDevAPI.Controllers;
using WebDevAPI.Db;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic.CardLogic;
using Xunit;

namespace WebDevApiTests
{
    public class RepositoryTest
    {
        private Mock<IPlayerRepository> mockPlayerRepository;
        private Player player;
        private Player player2;
        private List<Player> playerList;

        [SetUp]
        public void Setup()
        {
            mockPlayerRepository = new Mock<IPlayerRepository>();
            player = new Player
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "player",
                Email = "TestMail",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("TestPassword"),
                UserName = "TestUsername",
                Chips = 0,
                PokerTableId = Guid.NewGuid(),
            };
            player2 = new Player
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test2",
                LastName = "player2",
                Email = "TestMail2",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("TestPassword2"),
                UserName = "TestUsername2",
                Chips = 0,
                PokerTableId = Guid.NewGuid(),
            };
            playerList = new List<Player>
            {
                player,
                player2
            };
        }

        [Test]
        public void GetAllPlayers()
        {
            mockPlayerRepository.Setup(m => m.GetAll()).ReturnsAsync(playerList);

            var result = mockPlayerRepository.Object.GetAll().Result;

            mockPlayerRepository.Verify(m => m.GetAll(), Times.Once());

            Assert.That(result.Count, Is.EqualTo(2));

        }

        [Test]
        public void GetPlayerById()
        {

            mockPlayerRepository.Setup(m => m.Get(new Guid(player.Id))).ReturnsAsync(player);

            var result = mockPlayerRepository.Object.Get(new Guid(player.Id)).Result;

            mockPlayerRepository.Verify(m => m.Get(new Guid(player.Id)), Times.Once());

            Assert.IsNotNull(result);
        }
    }
}
