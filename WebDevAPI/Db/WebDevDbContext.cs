﻿using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Db
{
    public class WebDevDbContext : DbContext
    {
        public WebDevDbContext(DbContextOptions<WebDevDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne(b => b.PlayerHand)
                .WithOne(i => i.Player)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Player>()
                .HasOne(b=> b.PokerTable)
                .WithMany(i=> i.Players)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Card>()
                .HasOne(b => b.PlayerHand)
                .WithMany(i => i.Cards)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Card>()
                .HasOne(b => b.PokerTable)
                .WithMany(i => i.Cards)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Contactform> Contactforms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<PokerTable> PokerTables { get; set; }
        public DbSet<PlayerHand> PlayerHands { get; set; }
    }
}