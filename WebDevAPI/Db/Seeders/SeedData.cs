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
                if (context.Persons.Any())
                {
                    return;
                }
                context.Persons.AddRange(
                    new Models.Person
                    {
                        Id = Guid.NewGuid(),
                        Name = "TestPerson1",
                        Description = "Description1",
                        Age = 20,
                    },
                     new Models.Person
                     {
                         Id = Guid.NewGuid(),
                         Name = "TestPerson2",
                         Description = "Description2",
                         Age = 21,
                     },
                      new Models.Person
                      {
                          Id = Guid.NewGuid(),
                          Name = "TestPerson3",
                          Description = "Description3",
                          Age = 22,
                      },
                       new Models.Person
                       {
                           Id = Guid.NewGuid(),
                           Name = "TestPerson4",
                           Description = "Description4",
                           Age = 23,
                       },
                        new Models.Person
                        {
                            Id = Guid.NewGuid(),
                            Name = "TestPerson5",
                            Description = "Description5",
                            Age = 24,
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
