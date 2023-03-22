using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class PlayerHandRepository : BaseRepository<PlayerHand, Guid>, IPlayerHandRepository
    {
        public PlayerHandRepository(WebDevDbContext context) : base(context)
        {

        }
    }
}
