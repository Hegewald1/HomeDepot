using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeDepotWebApp.Models
{
    public class Tool
    {
        public int ToolId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int DepositPrice { get; set; }
        public int RentPrice { get; set; }
    }
}