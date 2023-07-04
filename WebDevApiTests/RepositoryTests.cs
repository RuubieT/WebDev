using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic.CardLogic;

namespace WebDevApiTests
{
    public class RepositoryTests
    {
        public Mock<IPlayerRepository> MockPlayerRepository;
        public Player Player;
        public Guid PlayerId;

        [SetUp]
        public void Setup()
        {
            MockPlayerRepository = new Mock<IPlayerRepository>();
            Player = new Player
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "Player",
                Email = "TestMail",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("TestPassword"),
                UserName = "TestUsername",
                Chips = 0,
                PokerTableId = Guid.NewGuid(),

            };

        }

        [Test]
        public void GetAllPlayers()
        {
            var playerList = new List<Player>();

            MockPlayerRepository.Setup(m => m.GetAll()).ReturnsAsync(playerList);
            var result = MockPlayerRepository.Object.GetAll();
            MockPlayerRepository.Verify(m => m.GetAll(), Times.Once());
        }

        [Test]
        public void Create_And_Get()
        {
            var createdPlayer = new Player();
            var player = new Player();

            MockPlayerRepository.Setup(m => m.Create(Player));
            MockPlayerRepository.Setup(m => m.Get(new Guid(Player.Id))).ReturnsAsync(player);
            var result = MockPlayerRepository.Object.Get(new Guid(Player.Id));
            MockPlayerRepository.Verify(m => m.Get(new Guid(Player.Id)), Times.Once());
        }
    }
}
