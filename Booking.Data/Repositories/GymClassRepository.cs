using Booking.Core.Models.Entities;
using Booking.Core.Repositories;
using Booking.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repositories
{
    public class GymClassRepository : IGymClassRepository
    {
        private readonly ApplicationDbContext db;

        public GymClassRepository(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<GymClass>> GetWithAttendingAsync()
        {
            return await db.GymClasses.Include(g => g.AttendingMembers).ToListAsync();
        }

        public async Task<IEnumerable<GymClass>> GetAsync()
        {
            return await db.GymClasses.ToListAsync();
        }

        public async Task<GymClass> GetAsync(int id)
        {
            return await db.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<GymClass> FindAsync(int? id)
        {
            return await db.GymClasses.FindAsync(id);
        }

        public void Add(GymClass gymClas)
        {
            db.Add(gymClas);
        }

        public void Update(GymClass gymClass)
        {
            db.Update(gymClass);
        }

        public void Remove(GymClass gymClass)
        {
            db.GymClasses.Remove(gymClass);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await db.GymClasses.AnyAsync(g => g.Id == id);
        }
    }
}
