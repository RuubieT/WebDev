using WebDevAPI.Db.Models;

namespace WebDevAPI.Db.Repositories.Contract
{
    public interface IPersonRepository : IBaseRepository<Person, Guid>
    {
    }
}
