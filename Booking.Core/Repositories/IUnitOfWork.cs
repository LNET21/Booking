using System.Threading.Tasks;

namespace Booking.Core.Repositories
{
    public interface IUnitOfWork
    {
        IApplicationUserGymsRepository AppUserGymClassRepository { get; }
        IGymClassRepository GymClassRepository { get; }

        Task CompleteAsync();
    }
}