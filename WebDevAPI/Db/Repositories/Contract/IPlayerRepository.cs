using WebDevAPI.Db.Models;

namespace WebDevAPI.Db.Repositories.Contract
{
    public interface IPlayerRepository : IBaseRepository<User, Guid>
    {
    }
}
