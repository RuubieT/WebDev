using WebDevAPI.Db;

namespace WebDevAPI.Db.Repositories
{
    public class UserRepository : BaseRepository<UserRepository, Guid>
    {
        public UserRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
