using WebDevAPI.Models;

namespace WebDevAPI.Repositories
{
    public class ContactFormRepository : BaseRepository<Contactform, Guid>
    {
        public ContactFormRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
