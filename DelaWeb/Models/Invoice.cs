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
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }
        public string Other4 { get; set; }
        public string Other5 { get; set; }
    }
}