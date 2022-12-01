using Microsoft.EntityFrameworkCore;
using Common.DataContexts;
using Core.Interfaces;

namespace Sigma_Software_Task.Repositories
{
    public class DBConfigGetRepository<T> : IGetRepository<T> where T : class
    {
        protected DBContext _dbContext;
        protected DbSet<T> ds;
        public DBConfigGetRepository(DataContextsHub dataContextsHub)
        {
            _dbContext = dataContextsHub.dbContext;
            ds = _dbContext.Set<T>();
        }
        public virtual IQueryable<T> GetAll()
        {
            return ds.AsNoTracking();
        }

        public virtual async Task<T> GetById(object id)
        {
            return await ds.FindAsync(id);
        }

    }
}
