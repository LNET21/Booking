using Booking.Core.Models.Entities;
using Booking.Core.Repositories;
using Booking.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    public class ApplicationUserGymsRepository : IApplicationUserGymsRepository
    {
        private readonly ApplicationDbContext db;

        public ApplicationUserGymsRepository(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ApplicationUserGymClass> FindAsync(int? id, string userId)
        {
            return await db.ApplicationUserGyms.FindAsync(userId, id);
        }
    }
}
