﻿using WebDevAPI.Db;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class PlayerRepository : BaseRepository<User, Guid>, IPlayerRepository
    {
        public PlayerRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
