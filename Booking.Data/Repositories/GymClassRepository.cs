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
    public class GymClassRepository : GenericRepository<GymClass>, IGymClassRepository
    {

        public GymClassRepository(ApplicationDbContext db) : base(db) { }
    }
}
