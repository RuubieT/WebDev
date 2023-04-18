using Microsoft.AspNetCore.Identity;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class PlayerRepository : BaseRepository<Player, Guid>, IPlayerRepository
    {
        public PlayerRepository(WebDevDbContext context) : base(context)
        {

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
