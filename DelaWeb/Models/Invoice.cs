using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelaWeb.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string Folio { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public int Percentage { get; set; }
        public int CustomerID { get; set; }
    }
}