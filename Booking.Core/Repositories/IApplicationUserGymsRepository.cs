using Booking.Core.Models.Entities;
using System.Threading.Tasks;

namespace Booking.Core.Repositories
{
    public interface IApplicationUserGymsRepository 
    {
        Task<ApplicationUserGymClass> FindAsync(int? id, string userId);
        void Add(ApplicationUserGymClass entity);
        void Remove(ApplicationUserGymClass entity);
    }
}