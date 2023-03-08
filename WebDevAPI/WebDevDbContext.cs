using Microsoft.EntityFrameworkCore;
using WebDevAPI.Models;

namespace WebDevAPI
{
    public class WebDevDbContext : DbContext
    {
        public WebDevDbContext(DbContextOptions<WebDevDbContext> options)
        : base(options)
        {
        }

        public DbSet<Contactform> Contactforms { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
