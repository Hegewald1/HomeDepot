using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeDepotWebApp.Models
{
    public class CustomerPageViewModel
    {
        public Customer Customer { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Tool> Tools { get; set; }
    }
}