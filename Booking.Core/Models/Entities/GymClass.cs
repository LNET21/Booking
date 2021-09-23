using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Models.Entities
{
    public class GymClass : Entity
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public TimeSpan? Duration { get; set; }
        public DateTime EndTime => StartDate.GetValueOrDefault() + Duration.GetValueOrDefault();
        public string Description { get; set; }

        public ICollection<ApplicationUserGymClass> AttendingMembers { get; set; }

       // public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
