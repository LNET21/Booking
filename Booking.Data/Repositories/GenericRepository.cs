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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        internal readonly ApplicationDbContext db;
        internal readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            dbSet = db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await dbSet.AnyAsync(g => g.Id == id);
        }

        public async Task<TEntity> FindAsync(int? id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void Update(TEntity entity)
        {
            db.Update(entity);
        }
    }
}
