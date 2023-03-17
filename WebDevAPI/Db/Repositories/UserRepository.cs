using WebDevAPI.Db;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
