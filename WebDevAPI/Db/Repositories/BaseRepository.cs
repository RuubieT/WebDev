using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Db.Repositories
{
    public class BaseRepository<T, TId> : IBaseRepository<T, TId> where T : class
    {
        protected readonly WebDevDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(WebDevDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual async Task<IList<T>> GetUsers(UserManager<IdentityUser> userManager)
        {
            var users = await userManager.GetUsersInRoleAsync("User");
            var myUsers = new List<T>();
            if(users != null)
            {
                foreach (var user in users)
                {
                    var entity = await _dbSet.FindAsync(Guid.Parse(user.Id));
                    if (entity != null)
                    {
                        myUsers.Add(entity);
                    }
                }

            };

            return myUsers; 
        }

        public virtual async Task<T> Get(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return null;

            return entity;
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<(bool succes, T result)> TryFind(Func<T, bool> predicate)
        {
            var query = _dbSet.Where(predicate);
            var succes = query.Count() > 0;
            var result = succes ? query.First() : null;
            return (succes, result);
        }

        public async Task<IList<T>> TryFindAll(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
    }
}
