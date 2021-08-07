using DelaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelaWeb.ViewModels
{
    public class OrdersViewModel
    {
        public OrdersViewModel()
        {
            Orders = new List<OrderViewModel>();
        }
        //public List<ItemDetails> Details { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }

    public class OrderViewModel
    {
        public string OrderID { get; set; }
        public string OrderDate { get; set; }
        public decimal Total { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int Items { get; set; }
    }
}