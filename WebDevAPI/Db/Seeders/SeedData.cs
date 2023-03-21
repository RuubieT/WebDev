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

                var descriptionList = new List<string>
                {
                    "Testbeschrijving",
                    "Even een leuke beschrijving verzinnen",
                    "Zie hier mijn beschijving"
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
                        Password = passwordList[rand.Next(0, passwordList.Count)],
                        Description = descriptionList[rand.Next(0, descriptionList.Count)],
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
                        Password = passwordList[rand.Next(0, passwordList.Count)],
                        Description = descriptionList[rand.Next(0, descriptionList.Count)],
                        Username = usernameList[rand.Next(0, usernameList.Count)],
                        Chips = rand.Next(0, 15000),
                    };
                    players.Add(player);
                }

                var deck = new DeckOfCards();
                deck.SetUpDeck(); //deck.getDeck returns 52 
                var cards = new List<Card>();
                for(int i = 0; i < deck.getDeck.Length; i++) 
                {
                    var card = new Card
                    {
                        CardId = deck.getDeck[i].CardId,
                        MySuit = deck.getDeck[i].MySuit,
                        MyValue = deck.getDeck[i].MyValue,
                    };
                    cards.Add(card);
                }

                context.Users.AddRange(users);
                context.Users.AddRange(players);
                context.SaveChanges();
            }
        }
    }
}
