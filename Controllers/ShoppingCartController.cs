using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bikevision.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Final()
        {
            return View();
        }
    }
}
