using DelaWeb.Models;
using DelaWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelaWeb.ViewModels
{
    public class CustomersGraphicViewModel
    {
        public CustomersGraphicViewModel()
        {
        }
        public CustomerStructure Parent { get; set; }
    }

    public class CustomerStructure
    {
        public CustomerStructure()
        {
            // Childs = new List<CustomerStructure>();
        }

        public int CustomerID { get; set; }
        public string Name { get; set; }
        public int SponsorID { get; set; }
        public string SponsorName { get; set; }

        public List<CustomerStructure> Childs
        {
            get
            {
                var list = new List<CustomerStructure>();
                var childs = Customers.GetCustomersBySponsorID(CustomerID);
                foreach (var child in childs)
                {
                    list.Add(new CustomerStructure { SponsorID = CustomerID, CustomerID = child.ID, Name = child.Name, SponsorName = Name });
                }
                return list;
            }
        }
    }
}