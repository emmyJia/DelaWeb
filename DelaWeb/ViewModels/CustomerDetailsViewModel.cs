﻿using DelaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelaWeb.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public CustomerDetailsViewModel()
        {
            Customer = new Customer();
            Invoices = new List<Invoice>();
            Orders = new List<Order>();
        }
        public Customer Customer { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Order> Orders { get; set; }
        public List<Customer> Childs { get; set; }
        public Customer Sponsor { get; set; }
    }
}