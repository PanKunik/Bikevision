using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using bikevision.Models;

namespace bikevision.Controllers
{
    public class ShopController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();
        // GET: Shop
        public ActionResult Index()
        {
            //var query = from items in db.Items
            //        join categories in db.Categories on items.Category_idCategory equals categories.idCategory
            //        //where categories.category1 == "Ramy rowerowe"
            //        select items;

            //return View(query);
            return View();
        }

        public ActionResult Product(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            //Item item = (from items in db.Items
            //             join categories in db.Categories on items.Category_idCategory equals categories.idCategory
            //             where items.idItem == id
            //             select items).First();

            //return View(item);
            return View();
        }
        public ActionResult ProductList()
        {
            //var query = from items in db.Items
            //            join categories in db.Categories on items.Category_idCategory equals categories.idCategory
            //            select items;

            return View();
        }

        public ActionResult Favorites()
        {
            return View();
        }
    }
}