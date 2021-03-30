using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DelaWeb.Models
{
    public class Order
    {
        public Order()
        {
            Date = DateTime.Now;
        }

        [Key]
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
        public int CustomerID { get; set; }
        public int Type { get; set; }
        public string Address { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }
        public string Other4 { get; set; }
        public string Other5 { get; set; }
        public List<OrderDetails> Details { get; set; }
    }
    public class OrderDetails
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int  ProductID { get; set; }
        public decimal Price { get; set; }
        public decimal Bonus { get; set; }
        public int Quantity { get; set; }
    }
}