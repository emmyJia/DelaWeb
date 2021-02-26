using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DelaWeb.Models;

namespace DelaWeb.Service
{
    public static class ProductsService
    {
        public static List<Product> GetAllProducts()
        {
            var list = new List<Models.Product>();
            using (var context = new  ApplicationDbContext())
            {
                list = (from c in context.Products
                            select c).ToList();
            }
            
            return list;
        }
        
        public static Product GetProductByID(int productID)
        {
            var invoice = new Models.Product();
            using (var context = new ApplicationDbContext())
            {
                invoice = (from c in context.Products
                        where c.ItemID == productID
                        select c).FirstOrDefault();
            }

            return invoice;
        }
        public static bool CreateProduct(Product c)
        {
            try
            {
                var invoice = new Customer();
                using (var context = new ApplicationDbContext())
                {
                    context.Products.Add(c);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        
        public static bool DeleteProduct(int productID)
        {
            try
            {
                var invoice = new Product();
                using (var context = new ApplicationDbContext())
                {
                    invoice = GetProductByID(productID);
                    if (invoice != null)
                    {
                        context.Products.Remove(invoice);
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