using App.Extensions;
using DelaWeb.Attributes;
using DelaWeb.Models;
using DelaWeb.Service;
using DelaWeb.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DelaWeb.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var model = new ViewModels.OrdersViewModel();
            var orders = db.Orders.ToList().OrderByDescending(i => i.Date);

            foreach (var item in orders)
            {
                var det = OrdersService.GetOrderDetailsByID(item.OrderID);
                if (det.Any())
                    model.Orders.Add(new OrderViewModel
                    {
                        OrderID = item.OrderID.ToString(),
                        OrderDate = item.Date.ToShortDateString(),
                        CustomerID = item.CustomerID,
                        CustomerName = Customers.GetCustomerByID(item.CustomerID).Name,
                        Items = det.Sum(i => i.Quantity),
                        Total = det.Sum(i => i.Price * i.Quantity)
                    });
            }
            return View(model);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new OrderDetailsViewModel();
            Order order = db.Orders.FirstOrDefault(c => c.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            order.Details = db.OrderDetails.Where(c => c.OrderID == id).ToList();
            model.CustomerName = Customers.GetCustomerByID(order.CustomerID).Name;
            model.Order = order;

            foreach (var item in order.Details)
            {
                var iteminfo = ProductsService.GetProductDescriptionByID(item.ProductID);
                var _item = new ItemDetails
                {
                    ItemCode = item.ID.ToString(),
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ItemDescription = iteminfo,
                };

                model.Details.Add(_item);
            }

            return View(model);
        }

        // GET: Orders/Create
        public ActionResult Create(int customerID = 0, string redirectTo = "")
        {
            ViewBag.CustomerID = customerID == 0 ? User.Identity.GetCustomerID() : customerID.ToString();
            ViewBag.RedirectTo = redirectTo;
            return View();
        }

        // POST: Orders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,Date,CustomerID,Type,Other1,Other2,Other3,Other4,Other5")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HttpPost]
        public ActionResult CreateOrder(int customerID, int type, string items)
        {
            try
            {

                var itemsObject = JArray.Parse(items);
                var listitems = itemsObject.ToObject<List<ItemCart>>();
                var order = new Order();
                order.CustomerID = customerID;
                order.Date = DateTime.Now;
                order.Type = type;
                var details = new List<OrderDetails>();


                //order.Details = details;
                order = db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in listitems.Where(i => i.Quantity > 0))
                {
                    var itemid = Int32.Parse(item.Code);
                    var itemDB = db.Products.FirstOrDefault(i => i.ItemID == itemid);
                    details.Add(new OrderDetails { ProductID = Int32.Parse(item.Code), OrderID = order.OrderID, Quantity = item.Quantity, Price = itemDB.Price });
                }

                db.OrderDetails.AddRange(details);
                db.SaveChanges();

                return new JsonNetResult(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new
                {
                    success = false
                });
            }
        }
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Date,CustomerID,Type,Other1,Other2,Other3,Other4,Other5")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult UpdateOrderStatus()
        {

            return View();
        }


        // GET: Orders/Create
        [AllowAnonymous]
        [HttpGet]
        public ActionResult CreateFlyOrder()
        {
            return View();
        }
        public void ValidateRequestHeader()
        {
            string cookieToken = "";
            string formToken = "";

            string tokenHeader = HttpContext.Request.Headers.Get("RequestVerificationToken");

            string[] tokens = tokenHeader.Split(':');

            if (tokens.Length == 2)
            {
                cookieToken = tokens[0].Trim();
                formToken = tokens[1].Trim();
            }

            AntiForgery.Validate(cookieToken, formToken);
        }

        [HttpPost]
        [CustomValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult CreateFlyOrder(string customerKey, string password, string items)
        {
            try
            {
                if (string.IsNullOrEmpty(customerKey) || string.IsNullOrEmpty(password))
                {
                    return RedirectToAction("Create");
                }

                var itemsObject = JArray.Parse(items);
                var listitems = itemsObject.ToObject<List<ItemCart>>();
                var order = new Order();
                var existingCustomer = new Customer();

                using (var context = new ApplicationDbContext())
                {

                    existingCustomer = (from c in context.Customers
                                        where c.Other1 == customerKey && c.Other2 == password
                                        select c).FirstOrDefault();
                }

                order.CustomerID = existingCustomer.ID;
                order.Date = DateTime.Now;
                order.Type = 1;
                var details = new List<OrderDetails>();

                //order.Details = details;
                order = db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in listitems.Where(i => i.Quantity > 0))
                {
                    var itemid = Int32.Parse(item.Code);
                    var itemDB = db.Products.FirstOrDefault(i => i.ItemID == itemid);
                    details.Add(new OrderDetails { ProductID = Int32.Parse(item.Code), OrderID = order.OrderID, Quantity = item.Quantity, Price = itemDB.Price });
                }

                db.OrderDetails.AddRange(details);
                db.SaveChanges();

                return new JsonNetResult(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new
                {
                    success = false
                });
            }
        }
    }
}
