using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DelaWeb.Models;

namespace DelaWeb.Service
{
    public static class InvoicesService
    {
        public static List<Invoice> GetAllInvoices()
        {
            var list = new List<Models.Invoice>();
            using (var context = new  ApplicationDbContext())
            {
                list = (from c in context.Invoices
                            select c).ToList();
            }
            
            return list;
        }
        public static List<Invoice> GetInvoicesByCustomerID(int customerID)
        {
            var list = new List<Models.Invoice>();
            using (var context = new ApplicationDbContext())
            {
                list = (from c in context.Invoices
                        where c.CustomerID==customerID
                        select c).ToList();
            }

            return list;
        }
        public static Invoice GetInvoiceByID(int invoiceID)
        {
            var invoice = new Models.Invoice();
            using (var context = new ApplicationDbContext())
            {
                invoice = (from c in context.Invoices
                        where c.InvoiceID == invoiceID
                        select c).FirstOrDefault();
            }

            return invoice;
        }
        public static bool CreateInvoice(Invoice c)
        {
            try
            {
                var invoice = new Customer();
                using (var context = new ApplicationDbContext())
                {
                    context.Invoices.Add(c);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        
        public static bool DeleteInvoice(int invoiceID)
        {
            try
            {
                var invoice = new Invoice();
                using (var context = new ApplicationDbContext())
                {
                    invoice = GetInvoiceByID(invoiceID);
                    if (invoice != null)
                    {
                        context.Invoices.Remove(invoice);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}