using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DelaWeb.Models;
using DelaWeb.Service;


namespace DelaWeb.Helpers
{
    public static class Helpers
    {
        public static List<SelectListItem> Products(this HtmlHelper helper)
        {
            var list = new List<SelectListItem>();
            var products = ProductsService.GetAllProducts();
            foreach (var item in products)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.ItemID.ToString() });
            }
            return list;
        }
    }
}