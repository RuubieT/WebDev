using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRoles, Guid>, IUserRoleRepository
    {
        public UserRoleRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
