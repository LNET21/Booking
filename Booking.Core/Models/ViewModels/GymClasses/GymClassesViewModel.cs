﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Models.ViewModels.GymClasses
{
   public class GymClassesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Attending { get; set; }
    }
}
