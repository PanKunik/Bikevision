using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bikevision.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
        // GET: Service/NewServiceOrder
        public ActionResult NewServiceOrder()
        {
            return View();
        }
        // GET: Service/CheckStatus
        public ActionResult CheckStatus()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Login", "Account");
        }
        // GET: Service/UserPanel
        public ActionResult UserPanel()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Account");
            else
                return RedirectToAction("Login", "Account");
        }
        // GET: Service/Prices
        public ActionResult Prices()
        {
            return View();
        }
    }
}