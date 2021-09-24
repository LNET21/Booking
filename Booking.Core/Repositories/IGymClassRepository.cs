using Booking.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.Core.Repositories
{
    public interface IGymClassRepository
    {
        void Add(GymClass gymClas);
        Task<IEnumerable<GymClass>> GetAsync();
        Task<GymClass> FindAsync(int? id);
        Task<GymClass> GetAsync(int id);
        void Update(GymClass gymClass);
        void Remove(GymClass gymClass);
        Task<bool> AnyAsync(int id);
        Task<IEnumerable<GymClass>> GetWithAttendingAsync();
        Task<IEnumerable<GymClass>> GetHistoryAsync();
    }
}