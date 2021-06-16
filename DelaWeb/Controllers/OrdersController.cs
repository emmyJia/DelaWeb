using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DelaWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DelaWeb.Service;

namespace DelaWeb.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.FirstOrDefault(c=>c.OrderID == id);
            order.Details = db.OrderDetails.Where(c => c.OrderID == id).ToList();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create(int customerID = 0, string redirectTo = "")
        {
            ViewBag.CustomerID = customerID;
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
                    var itemDB = db.Products.Find(item.Code);
                    details.Add(new OrderDetails { ProductID = Int32.Parse(item.Code), OrderID = order.OrderID, Quantity = item.Quantity, Price = itemDB.Price });
                }

                db.OrderDetails.AddRange(details);
                db.SaveChanges();

                return new JsonNetResult(new { 
                    success = true
                });
            }
            catch(Exception ex)
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
    }
}
