using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class PlayerRepository : BaseRepository<Player, Guid>, IPlayerRepository
    {
        public PlayerRepository(WebDevDbContext context) : base(context)
        {
        }

        public virtual async Task<Player> Get(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return null;

            return entity;
        }

        public async Task IdentityUserToPlayer(IdentityUser oldUser, Player newPlayer, UserManager<IdentityUser> usermanager)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            await usermanager.DeleteAsync(oldUser);
            await Create(newPlayer);

            await transaction.CommitAsync();
        }


    }
}
