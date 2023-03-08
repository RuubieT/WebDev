using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IBaseRepository<T, TId> where T : class
    {
        public Task<IList<T>> GetAll();

        public Task<T> Get(TId id);

        public Task Update(T entity);

        public Task Create(T entity);

        public Task Delete(T entity);

        public Task<(bool succes, T? result)> TryFind(Func<T, bool> predicate);

    }
}