using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Db
{
    public class WebDevDbContext : IdentityDbContext<IdentityUser>
    {
        public WebDevDbContext(DbContextOptions<WebDevDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Player>()
                .HasOne(b => b.PlayerHand)
                .WithOne(i => i.Player)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Contactform> Contactforms { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<PokerTable> PokerTables { get; set; }
        public DbSet<PlayerHand> PlayerHands { get; set; }
    }
}
