using CoachTwinsApi.Db.Repository.Contract;
using WebDevAPI.Models;

namespace WebDevAPI.Repositories.Contract
{
    public interface IPersonRepository : IBaseRepository<Person, Guid>
    {
    }
}
