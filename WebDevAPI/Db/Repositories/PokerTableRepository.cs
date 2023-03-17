using WebDevAPI.Db;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class PokerTableRepository : BaseRepository<User, Guid>, IPokerTableRepository
    {
        public PokerTableRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
