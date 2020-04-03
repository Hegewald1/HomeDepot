using HomeDepotWebApp.Models;
using HomeDepotWebApp.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeDepotDesktopApp
{
    public partial class MainWindow : Window
    {
        private HomeDepotContext _context;
        private Customer _selectedCustomer;
        private Booking _selectedBooking;

        public MainWindow()
        {
            InitializeComponent();
            _context = new HomeDepotContext();
            CustomerListBox.ItemsSource = _context.Customers.ToList();
        }
        
        private void Button_CreateCustomer(object sender, RoutedEventArgs e)
        {
            if(CustomerName.Text != null && CustomerAdr.Text != null && CustomerEmail.Text != null)
            {
                if (!new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$").IsMatch(CustomerEmail.Text))
                {
                    MessageBox.Show("Invalid email adress");
                    return;
                }
                if (_context.Customers.ToList().Find(c => c.Email.Equals(CustomerEmail.Text)) == null)
                {
                    Customer customer = new Customer { Name = CustomerName.Text, Adress = CustomerAdr.Text, Email = CustomerEmail.Text , Password = "123"};
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    MessageBox.Show("Customer Created");
                    //listbox doesnt refresh without resetting the itemssource
                    CustomerListBox.ItemsSource = _context.Customers.ToList();
                    CustomerListBox.Items.Refresh();
                } else
                {
                    MessageBox.Show("Customer with that name already exists");
                }
            }
        }

        //Edits the currently selected customer
        private void Button_EditCustomer(object sender, RoutedEventArgs e)
        {
            if (CustomerName.Text != null && CustomerAdr.Text != null && CustomerEmail.Text != null && _selectedCustomer != null)
            {
                if (!new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$").IsMatch(CustomerEmail.Text))
                {
                    MessageBox.Show("Invalid email adress");
                    return;
                }
                    var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == _selectedCustomer.CustomerId);
                    if(customer != null)
                    {
                        customer.Name = CustomerName.Text;
                        customer.Adress = CustomerAdr.Text;
                        customer.Email = CustomerEmail.Text;
                        _context.SaveChanges();
                        CustomerListBox.Items.Refresh();
                    MessageBox.Show("Customer Edited");
                }
            }
            else
            {
                MessageBox.Show("Customer is not selected or there is an empty field");
            }
            
        }

        private void Button_BookingChangeStatus(object sender, RoutedEventArgs e)
        {
            if(_selectedBooking == null)
            {
                MessageBox.Show("No booking selected");
                return;
            }
            string buttonContent = (sender as Button).Content.ToString();
            
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == _selectedBooking.BookingId);
            if(booking != null)
            {
                booking.Status = buttonContent;
                _context.SaveChanges();
                BookingStatus.Text = buttonContent;
             }
        }

        private void CustomerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCustomer = CustomerListBox.SelectedItem.ToString();
            int id = Convert.ToInt32(selectedCustomer.Split(' ')[1]);
            _selectedCustomer = _context.Customers.Find(id);
            //_selectedCustomer = _context.Customers.ToList().Find(c => c.Name.Equals(selectedCustomer));
            if(_selectedBooking != null)
            {
                BookingListBox.SelectedItem = null;
            }
            if(_selectedCustomer != null)
            {
                BookingListBox.ItemsSource = _context.Bookings
                .Where(c => _selectedCustomer.CustomerId == c.CustomerId)
                .OrderBy(d => d.PickupDay).ToList(); //sorts list by date
                CustomerName.Text = _selectedCustomer.Name;
                CustomerAdr.Text = _selectedCustomer.Adress;
                CustomerEmail.Text = _selectedCustomer.Email;
            }
            
        }

        private void BookingListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BookingListBox.SelectedItem != null)
            {
                string[] booking = BookingListBox.SelectedItem.ToString().Split(' ');
                _selectedBooking = _context.Bookings.ToList().Find(b => b.BookingId == Convert.ToInt32(booking[1]));

                BookingPickup.Text = _selectedBooking.PickupDay.ToString("dd/MM/yyyy");
                BookingDays.Text = _selectedBooking.Days.ToString();
                BookingStatus.Text = _selectedBooking.Status;

                Tool tool = _context.Tools.ToList().Find(t => t.ToolId == _selectedBooking.ToolId);
                BookingTool.Text = tool.Name;
                BookingDescr.Text = tool.Description;
                BookingPrice.Text = (tool.DepositPrice + (_selectedBooking.Days * tool.RentPrice)).ToString();
            }

        }
    }
}
