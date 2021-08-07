using App.Extensions;
using System.Web.Mvc;

namespace DelaWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.isMaster = User.Identity.IsAuthenticated && User.Identity.IsMaster();
            return View();
        }
        public ActionResult Start()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}