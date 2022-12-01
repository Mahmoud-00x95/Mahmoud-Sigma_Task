using Microsoft.EntityFrameworkCore;
using Common.DataContexts;
using Core.Interfaces;

namespace Sigma_Software_Task.Repositories
{
    public class DBConfigGetService<T> : IGetService<T> where T : class
    {
        protected IGetRepository<T> _getRepository;
        public DBConfigGetService(IGetRepository<T> getRepository)
        {
            _getRepository = getRepository;
        }
    }
}
