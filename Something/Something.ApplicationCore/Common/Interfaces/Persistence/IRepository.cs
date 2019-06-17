using System.Collections.Generic;
using System.Threading.Tasks;

namespace Byui.Something.ApplicationCore.Common.Interfaces.Persistence
{
    public interface IRepository<T> where T: Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> GetByIdAsync(int id);
        Task<T> GetSingleBySpecAsync(ISpecification<T> spec);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAsync(ISpecification<T> spec, int? skip = null, int? take = null, bool? distinct = null);
        Task<int> Count(ISpecification<T> spec);
        void Add(T entity);
        void Add(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Detach(T entity);
        void Delete(IEnumerable<T> entities);
    }
}