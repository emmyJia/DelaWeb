using DelaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelaWeb.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel()
        {
            Details = new List<ItemDetails>();
        }
        public List<ItemDetails> Details { get; set; }
        public Order Order { get; set; }
        public string CustomerName { get; set; }
    }

    public class ItemDetails
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}