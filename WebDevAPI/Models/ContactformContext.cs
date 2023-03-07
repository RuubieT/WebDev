using Microsoft.EntityFrameworkCore;

namespace WebDevAPI.Models
{
    public class ContactformContext: DbContext
    {
        public ContactformContext(DbContextOptions<ContactformContext> options)
        : base(options)
        {
        }

        public DbSet<ContactformModel> Contactforms { get; set; } = null!;
    }
}
