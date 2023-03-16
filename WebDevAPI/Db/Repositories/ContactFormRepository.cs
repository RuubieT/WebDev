using WebDevAPI.Db;
using WebDevAPI.Db.Dto_s.Contactform;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Db.Repositories
{
    public class ContactFormRepository : BaseRepository<Contactform, Guid>
    {
        public ContactFormRepository(WebDevDbContext context) : base(context)
        {

        }
    }
}
