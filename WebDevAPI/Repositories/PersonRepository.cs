namespace WebDevAPI.Repositories
{
    public class PersonRepository : BaseRepository<PersonRepository, Guid>
    {
        public PersonRepository(WebDevDbContext context) : base(context)
        {
        }
    }
}
