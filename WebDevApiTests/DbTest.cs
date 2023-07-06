using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using WebDevAPI.Controllers;
using WebDevAPI.Db;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic.CardLogic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebDevApiTests
{
    public class PlayerControllerTests
    {
        private DbContextOptions<WebDevDbContext> options;
        private Mock<WebDevDbContext> dbContextMock;



        [SetUp]
        public void Setup()
        {
            dbContextMock = new Mock<WebDevDbContext>();
            options = new DbContextOptionsBuilder<WebDevDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB")
                .Options;

        }


        [Test]
        public void GetPlayersReturnPlayers()
        {
            // Arrange
            using (var context = new WebDevDbContext(options))
            {
                // Populate the in-memory database with some test data
                var expectedData = new List<Player>
                {
                    new Player
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Test",
                        LastName = "player",
                        Email = "TestMail",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("TestPassword"),
                        UserName = "TestUsername",
                        Chips = 0,
                        PokerTableId = Guid.NewGuid(),
                        AuthCode = "FABGJAG",
                    }
                };
                context.Players.AddRange(expectedData);
                context.SaveChanges();
                Assert.AreEqual(context.Players.Count(), 1);

            }
        }
    }
}
