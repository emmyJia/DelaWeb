using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DelaWeb.Models
{
    public class Bonus
    {
        public int CustomerID { get; set; }
        [Key]
        public int BonusID { get; set; }
        public decimal BonusTotal { get; set; }
        public DateTime Date { get; set; }
        public string AddRest { get; set; }
        public decimal Quantity { get; set; }
        public int OrderID { get; set; }

    }
}