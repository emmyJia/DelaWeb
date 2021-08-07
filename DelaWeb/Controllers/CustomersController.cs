using DelaWeb.Models;
using DelaWeb.Service;
using DelaWeb.ViewModels;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace DelaWeb.Controllers
{
    [Route("customers")]
    [Authorize]
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var model = new CustomersViewModel();
            model.CustomersList = Customers.GetAllCustomers();
            return View(model);
        }
        //public ActionResult CreateAll()
        //{
        //    using (var db = new ApplicationDbContext())
        //    {

        //        var model = new CustomersViewModel();
        //        model.CustomersList = Customers.GetAllCustomers();
        //        AccountController ac = new AccountController();
        //        var Response = false;
        //        foreach (var item in model.CustomersList.Where(c => c.ID > 82))
        //        {
        //            Response = ac.RegisterOne(item.ID).GetAwaiter().GetResult();
        //        }
        //    }

        //    return View("Index", "Home");
        //}
        // GET: Customers/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            var model = new CustomerDetailsViewModel();
            model.Customer = Customers.GetCustomerByID(id);
            model.Invoices = InvoicesService.GetInvoicesByCustomerID(id);
            model.Orders = OrdersService.GetOrdersByCustomerID(id);
            model.Childs = Customers.GetCustomersBySponsorID(id);
            model.Sponsor = Customers.GetCustomerByID(model.Customer.SponsorID);

            return View(model);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var creationRequest = new Customer();
                creationRequest.Name = Convert.ToString(collection["Name"]);
                creationRequest.Address1 = Convert.ToString(collection["Address1"]);
                creationRequest.Phone = Convert.ToString(collection["Phone"]);
                creationRequest.SponsorID = Convert.ToInt32(collection["SponsorID"]);

                if (Customers.CreateCustomer(creationRequest))
                {
                    //AccountController ac = new AccountController();
                    //var Response = ac.RegisterOne(new RegisterViewModel { CustomerID = item.ID, Email = item.ID + "@dela.com", Password = "Dela1234." }).GetAwaiter().GetResult();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        [Route("details/{id}")]

        public ActionResult Edit(int id)
        {
            var model = new Customer();
            model = Customers.GetCustomerByID(id);
            return View(model);
        }

        // POST: Customers/Edit/5
        //public ActionResult Edit([Bind(Include = "ItemID,ItemCode,Name,Description,Price,DiscountPrice,Bonus,Type,Other1,Other2,Other3,Other4,Other5")] Product product)
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Name,Address1,Phone,SponsorID")] Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Customers.EditCustomer(customer);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CustomersGraphic(int customerID = 0)
        {
            return View();
        }
        public ActionResult GetCustomersGraphic(int customerID = 0)
        {
            var model = new CustomersGraphicViewModel();
            var parent = Customers.GetCustomerByID(customerID);
            model.Parent = Customers.GetCustomerStructure(customerID);

            string output = JsonConvert.SerializeObject(model.Parent);

            return new JsonNetResult(new
            {
                success = true,
                structure = output
            });
        }
    }
}
