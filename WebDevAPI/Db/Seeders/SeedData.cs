using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using WebDevAPI.Db;
using WebDevAPI.Db.Models;
using WebDevAPI.Logic.CardLogic;
using static WebDevAPI.Db.Models.Card;

namespace WebDevAPI.Db.Seeders

{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebDevDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WebDevDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }
                var rand = new Random();

                var firstnames = new List<string>
                {
                    "Pieter",
                    "Henk",
                    "Bas",
                };

                var lastnames = new List<string>
                {
                    "De Jong",
                    "Frederiksen",
                    "Berritsen",
                };

                var emails = new List<string>
                {
                    "email1@gmail.com",
                    "email2@hotmail.nl",
                    "email3@outlook.com",
                };

                var passwordList = new List<string>
                {
                    "Password123",
                    "SecretPassword",
                    "NewPassword"
                };

                var usernameList = new List<string>
                {
                    "Winner123",
                    "RealGamer",
                    "___TROLL___"
                };

                var users = new List<User>();
                for (var i = 0; i < 5; i++)
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstnames[rand.Next(0, firstnames.Count)],
                        LastName = lastnames[rand.Next(0, lastnames.Count)],
                        Email = emails[rand.Next(0, emails.Count)],
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordList[rand.Next(0, passwordList.Count)]),
                    };
                    users.Add(user);
                }

                var players = new List<Player>();
                for (var i = 0; i < 5; i++)
                {
                    var player = new Player
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstnames[rand.Next(0, firstnames.Count)],
                        LastName = lastnames[rand.Next(0, lastnames.Count)],
                        Email = emails[rand.Next(0, emails.Count)],
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordList[rand.Next(0, passwordList.Count)]),
                        Username = usernameList[rand.Next(0, usernameList.Count)],
                        Chips = rand.Next(0, 15000),
                        PokerTableId = new Guid("539F9F45-53A7-4087-B728-8F664E765F92"),
                    };
                    players.Add(player);
                }
                
                #region Setup nullable field
                context.PokerTables.Add(new PokerTable
                {
                    PokerTableId = new Guid("539F9F45-53A7-4087-B728-8F664E765F92"),
                    Ante = 10,
                    SmallBlind = 20,
                    BigBlind = 30,
                    MaxSeats = 8,
                });
                context.Users.Add(new Player
                {
                    Id = new Guid("DA8289A8-3382-429B-B915-31989D6F7FC8"),
                    FirstName = firstnames[rand.Next(0, firstnames.Count)],
                    LastName = lastnames[rand.Next(0, lastnames.Count)],
                    Email = emails[rand.Next(0, emails.Count)],
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordList[rand.Next(0, passwordList.Count)]),
                    Username = usernameList[rand.Next(0, usernameList.Count)],
                    Chips = rand.Next(0, 15000),
                    PokerTableId = new Guid("539F9F45-53A7-4087-B728-8F664E765F92"),
                });

                context.PlayerHands.Add(new PlayerHand
                {
                    PlayerHandId = new Guid("DB8289A8-3382-429B-B915-31989D6F7FC8"),
                    PlayerId = new Guid("DA8289A8-3382-429B-B915-31989D6F7FC8"),
                });
                context.SaveChanges();
                #endregion

                var deck = new DeckOfCards();
                deck.SetUpDeck(); //deck.getDeck returns 52 
                var cards = new List<Card>();
                for (int i = 0; i < 52; i++)
                {
                    var card = deck.getDeck.Dequeue();
                    cards.Add(card);
                }

                context.Cards.AddRange(cards);
                context.Users.AddRange(users);
                context.Users.AddRange(players);
                context.SaveChanges();
            }
        }
    }
}
