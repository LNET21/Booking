using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Models.Entities
{
   public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public DateTime TimeOfRegistration { get; set; }
        public ICollection<ApplicationUserGymClass> AttendedClasses { get; set; }

    }
}
