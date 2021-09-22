using Booking.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public IGymClassRepository GymClassRepository { get; }
        public IApplicationUserGymsRepository AppUserGymClassRepository { get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            GymClassRepository = new GymClassRepository(db);
            AppUserGymClassRepository = new ApplicationUserGymsRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
