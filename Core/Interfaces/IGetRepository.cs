using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGetRepository<T> where T : class
    {
        Task<T> GetById(object id);
        IQueryable<T> GetAll();
    }
}
