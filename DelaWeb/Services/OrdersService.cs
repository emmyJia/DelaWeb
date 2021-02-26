using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DelaWeb.Models;

namespace DelaWeb.Service
{
    public static class OrdersService
    {
        public static List<Order> GetAllOrders()
        {
            var list = new List<Order>();
            using (var context = new  ApplicationDbContext())
            {
                list = (from c in context.Orders
                            select c).ToList();
            }
            
            return list;
        }
        public static List<Order> GetOrdersByCustomerID(int customerID)
        {
            var list = new List<Order>();
            using (var context = new ApplicationDbContext())
            {
                list = (from c in context.Orders
                        where c.CustomerID==customerID
                        select c).ToList();
            }

            return list;
        }
        public static Order GetOrderByID(int OrderID)
        {
            var order = new Order();
            using (var context = new ApplicationDbContext())
            {
                order = (from c in context.Orders
                        where c.OrderID == OrderID
                        select c).FirstOrDefault();
            }

            return order;
        }
        public static bool CreateOrder(Order c)
        {
            try
            {
                var order = new Customer();
                using (var context = new ApplicationDbContext())
                {
                    context.Orders.Add(c);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        
        public static bool DeleteOrder(int OrderID)
        {
            try
            {
                var order = new Order();
                using (var context = new ApplicationDbContext())
                {
                    order = GetOrderByID(OrderID);
                    if (order != null)
                    {
                        context.Orders.Remove(order);
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