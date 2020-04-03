using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeDepotWebApp.Models
{
    public class CreateBookingViewModel
    {
        public List<Tool> Tools { get; set; }
        public Booking Booking;
    }
}