using Booking.Core.Models.Entities;
using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    public interface IApplicationUserGymsRepository
    {
        Task<ApplicationUserGymClass> FindAsync(int? id, string userId);
    }
}