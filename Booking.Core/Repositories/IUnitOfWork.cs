using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    public interface IUnitOfWork
    {
        IApplicationUserGymsRepository AppUserGymClassRepository { get; }
        IGymClassRepository GymClassRepository { get; }

        Task CompleteAsync();
    }
}