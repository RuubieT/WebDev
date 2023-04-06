using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class RoleRepository : BaseRepository<Roles, Guid>, IRoleRepository
    {
        public RoleRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
