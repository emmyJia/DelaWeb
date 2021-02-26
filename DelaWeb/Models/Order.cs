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
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }
        public string Other4 { get; set; }
        public string Other5 { get; set; }
        public OrderDetails Details { get; set; }
    }
    public class OrderDetails
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public List<Product> Products { get; set; }
    }
}