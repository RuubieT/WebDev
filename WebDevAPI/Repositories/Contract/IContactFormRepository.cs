using CoachTwinsApi.Db.Repository.Contract;
using WebDevAPI.Models;

namespace WebDevAPI.Repositories.Contract
{
    public interface IContactFormRepository: IBaseRepository<Contactform, Guid>
    {
    }
}
