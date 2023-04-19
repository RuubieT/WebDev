using Microsoft.AspNetCore.Identity;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Db.Repositories.Contract
{
    public interface IPlayerRepository : IBaseRepository<Player, Guid>
    {
        public Task IdentityUserToPlayer(IdentityUser oldUser, Player newPlayer, UserManager<IdentityUser> usermanager);
    }
}
