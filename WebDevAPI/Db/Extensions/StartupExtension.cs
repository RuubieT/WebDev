using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDevAPI.Db.Repositories;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Extensions
{
    public static class StartupExtension
    {
        public static void ConfigureEf(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<WebDevDbContext>(options =>
               options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        x => x.MigrationsAssembly("TestDB.Db")));
        }

        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<IContactFormRepository, ContactFormRepository>();
            serviceCollection.AddTransient<IPlayerRepository, PlayerRepository>();
            serviceCollection.AddTransient<IPokerTableRepository, PokerTableRepository>();
            serviceCollection.AddTransient<ICardRepository, CardRepository>();
            serviceCollection.AddTransient<IPlayerHandRepository, PlayerHandRepository>();
            serviceCollection.AddTransient<IRoleRepository, RoleRepository>();
            serviceCollection.AddTransient<IUserRoleRepository, UserRoleRepository>();
        }
    }
}
