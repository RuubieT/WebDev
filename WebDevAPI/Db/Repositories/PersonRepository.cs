using WebDevAPI.Db;

namespace WebDevAPI.Db.Repositories
{
    public class PersonRepository : BaseRepository<PersonRepository, Guid>
    {
        public PersonRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
