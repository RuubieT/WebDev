using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class CardRepository : BaseRepository<Card, Guid>, ICardRepository
    {
        public CardRepository(WebDevDbContext context) : base(context)
        {

        }
    }
}
