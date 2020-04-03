using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeDepotWebApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        [Required(ErrorMessage = "Pickup day is required")]
        [DataType(DataType.DateTime)]
        public DateTime PickupDay { get; set; }
        [Required(ErrorMessage = "Days is required")]
        public int Days { get; set; }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Tool is required")]
        public int ToolId { get; set; }
        public String Status { get; set; }

        public override string ToString()
        {
            return "Id: " + BookingId + " Date: " + PickupDay.ToString("dd/MM/yyyy");
        }
        
    }

}