using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebDevAPI.Db;

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
                context.Users.AddRange(
                    new Models.User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Test",
                        LastName = "Person",
                        Email = "test@test.nl",
                        Description = "Description1",
                    },
                     new Models.User
                     {
                         Id = Guid.NewGuid(),
                         FirstName = "Tist",
                         LastName = "Person2",
                         Email = "test@test.com",
                         Description = "Description2",
                     },
                      new Models.User
                      {
                          Id = Guid.NewGuid(),
                          FirstName = "Tost",
                          LastName = "Person3",
                          Email = "test@test.com",
                          Description = "Description3",
                      },
                       new Models.User
                       {
                           Id = Guid.NewGuid(),
                           FirstName = "T3st",
                           LastName = "Person4",
                           Email = "test@test.com",
                           Description = "Description4",
                       },
                        new Models.User
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "Tester",
                            LastName = "Person5",
                            Email = "test@test.com",
                            Description = "Description5",
                        });
                context.Contactforms.AddRange(
                    new Models.Contactform
                    {
                        Id = Guid.NewGuid(),
                        Name = "TestPerson1",
                        Email = "email@email.nl",
                        Subject = "ASubject",
                        Description = "Description1",
                    },
                    new Models.Contactform
                    {

                        Id = Guid.NewGuid(),
                        Name = "TestPerson2",
                        Email = "emailer@email.com",
                        Subject = "AnotherSubject",
                        Description = "Description2",
                    },
                    new Models.Contactform
                    {

                        Id = Guid.NewGuid(),
                        Name = "TestPerson3",
                        Email = "email@at.de",
                        Subject = "Wowsubj",
                        Description = "Description3",
                    },
                    new Models.Contactform
                    {

                        Id = Guid.NewGuid(),
                        Name = "TestPerson4",
                        Email = "email@email.last",
                        Subject = "ASubjectwel",
                        Description = "Description4",
                    },
                    new Models.Contactform
                    {

                        Id = Guid.NewGuid(),
                        Name = "TestPerson5",
                        Email = "myemailer@email.nl",
                        Subject = "ASubjecttotalk",
                        Description = "Description5",
                    });
                context.SaveChanges();
            }
        }
    }
}
