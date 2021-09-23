using Booking.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.Core.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        void Add(TEntity entity);
        Task<bool> AnyAsync(int id);
        Task<TEntity> FindAsync(int? id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}