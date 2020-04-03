using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeDepotWebApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public String Name { get; set; }
        public String Adress { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public override string ToString()
        {
            return "Id: " + CustomerId + " " + Name;
        }
    }
}