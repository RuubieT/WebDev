using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class ContactFormRepository : BaseRepository<Contactform, Guid>, IContactFormRepository
    {
        public ContactFormRepository(WebDevDbContext context) : base(context)
        {

        }
    }
}
