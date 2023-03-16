﻿using Microsoft.EntityFrameworkCore;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Db
{
    public class WebDevDbContext : DbContext
    {
        public WebDevDbContext(DbContextOptions<WebDevDbContext> options)
        : base(options)
        {
        }

        public DbSet<Contactform> Contactforms { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
