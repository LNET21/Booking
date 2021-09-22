using Booking.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    public interface IGymClassRepository
    {
        void Add(GymClass gymClas);
        Task<IEnumerable<GymClass>> GetAsync();
        Task<GymClass> FindAsync(int? id);
    }
}