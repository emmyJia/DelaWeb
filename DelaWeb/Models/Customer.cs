using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelaWeb.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public int ID { get; set; }
        public int SponsorID { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime StartTime { get; set; }
    }

    public enum CustomerType
    {
        Distributor = 1,
        Preferred = 2,
        Retail = 3
    }
}