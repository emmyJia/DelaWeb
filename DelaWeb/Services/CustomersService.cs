using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DelaWeb.Models;

namespace DelaWeb.Service
{
    public static class Customers
    {
        public static List<Customer> GetAllCustomers()
        {
            var list = new List<Models.Customer>();
            using (var context = new ApplicationDbContext())
            {
                list = (from c in context.Customers
                            select c).ToList();
            }
            
            return list;
        }
        public static Customer GetCustomerByID(int customerID)
        {
            var customer = new Models.Customer();
            using (var context = new ApplicationDbContext())
            {
                customer = (from c in context.Customers
                        where c.ID == customerID
                        select c).FirstOrDefault();
            }

            return customer;
        }
        public static bool CreateCustomer(Customer c)
        {
            try
            {
                var customer = new Customer();
                using (var context = new ApplicationDbContext())
                {
                    context.Customers.Add(c);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static bool EditCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = new Customer();
                using (var context = new ApplicationDbContext())
                {
                    existingCustomer = (from c in context.Customers
                                where c.ID == customer.ID
                                select c).FirstOrDefault();

                    existingCustomer.Address1 = customer.Address1;
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Phone = customer.Phone;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static bool DeleteCustomer(int customerID)
        {
            try
            {
                var customer = new Customer();
                using (var context = new ApplicationDbContext())
                {
                    customer = GetCustomerByID(customerID);
                    if (customer != null)
                    {
                        context.Customers.Remove(customer);
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