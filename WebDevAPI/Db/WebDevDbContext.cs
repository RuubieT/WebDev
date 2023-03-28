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
                .HasOne(b=> b.PokerTable)
                .WithMany(i=> i.Players)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Contactform> Contactforms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PokerTable> PokerTables { get; set; }
    }
}
