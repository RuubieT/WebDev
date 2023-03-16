using WebDevAPI.Db.Models;

namespace WebDevAPI.Db.Repositories.Contract
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
    }
}
