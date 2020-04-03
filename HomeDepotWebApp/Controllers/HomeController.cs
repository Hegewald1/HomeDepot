using HomeDepotWebApp.Models;
using HomeDepotWebApp.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeDepotWebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            if (ModelState.IsValid)
            {
                using(HomeDepotContext context = new HomeDepotContext())
                {
                    //probably doesnt work
                    
                    var cust = context.Customers.ToList().Find(c => c.Email.Equals(customer.Email) && c.Password.Equals(customer.Password));
                    if(cust != null)
                    {
                        CustomerPageViewModel customerPage = new CustomerPageViewModel();
                        customerPage.Customer = cust;
                        customerPage.Bookings = context.Bookings.ToList().FindAll(b => b.CustomerId == cust.CustomerId);
                        customerPage.Tools = context.Tools.ToList();
                        Session["customerPage"] = customerPage;
                        return View("CustomerPage", customerPage);
                    } else
                    {
                        ViewBag.Error = "Invalid login info";
                    }
                }
            }
            return View("Login");
        }
        public ActionResult CustomerPage(CustomerPageViewModel customerPage)
        {
            if(Session["customerPage"] != null)
            {
                CustomerPageViewModel sCustomerPage = Session["customerPage"] as CustomerPageViewModel;
                using (HomeDepotContext context = new HomeDepotContext())
                {
                    sCustomerPage.Bookings = context.Bookings.ToList().FindAll(b => b.CustomerId == sCustomerPage.Customer.CustomerId);
                }
                    
                return View(Session["customerPage"] as CustomerPageViewModel);
            } 
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        public ActionResult Logout()
        {
            if(Session["customerPage"] != null)
            {
                Session["customerPage"] = null;
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult CreateBooking()
        {
            List<SelectListItem> toolNames = new List<SelectListItem>();
            using (HomeDepotContext context = new HomeDepotContext())
            {
                foreach(var tool in context.Tools.ToList())
                {
                    toolNames.Add(new SelectListItem { Text = tool.Name });
                }
            }
            toolNames.OrderBy(t => t.Text);
            Session["toolNames"] = toolNames;
            return View();
        }

        [HttpPost]
        public ActionResult CreateBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateBooking");
            }
            
            using (HomeDepotContext context = new HomeDepotContext())
            {
                //skal gemme en booking i db og redirecte til customerpage (eventuelt besked med at booking er oprettet + fejl beskeder for variabler)

                string selectedTool = Request.Form["ToolNames"].ToString();
                Tool tool = context.Tools.ToList().Find(t => t.Name.Equals(selectedTool));
                List<Booking> toolBookings = context.Bookings.ToList().FindAll(b => b.ToolId == tool.ToolId);
                foreach (var b in toolBookings)
                {
                    for (int i = 0; i < b.Days; i++)
                    {
                        if (b.PickupDay.AddDays(i) == booking.PickupDay && b.PickupDay == booking.PickupDay.AddDays(i))
                        {
                            ViewBag.Error = "Tool already booked for this period \n Available after: " + b.PickupDay.ToString("dd/MM/yyy/");
                            return View();
                        }
                    }
                }
                if (Session["customerPage"] == null)
                {
                    return View("Login");
                }
                else
                {
                    CustomerPageViewModel customerPage = Session["customerPage"] as CustomerPageViewModel;
                    context.Bookings.Add(new Booking { PickupDay = booking.PickupDay, CustomerId = customerPage.Customer.CustomerId, Status = "Reserveret", Days = booking.Days, ToolId = tool.ToolId });
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Customerpage", Session["customerPage"]);
            //return View("CustomerPage", Session["customerPage"]);
        }
    }
}