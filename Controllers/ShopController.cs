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
        public ActionResult Index(string keywords)
        {
            CarouselItemsViewModel itemList = new CarouselItemsViewModel();
            
            itemList.ItemsOnPromotion = db.Items.Where(i => i.discount > 0).Take(15).ToList();
            itemList.NewestItems = db.Items.OrderByDescending(i => i.idItem).Take(15).ToList();
            itemList.PopularItems = db.Items.Where(i => i.name.Contains("asdasd")).ToList();

            if (itemList != null)
            {
                return View(itemList);
            }

            return View();
        }

        public ActionResult Product(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            Item item = (from items in db.Items
                         join categories in db.Categories on items.Category_idCategory equals categories.idCategory
                         where items.idItem == id
                         select items).First();

            return View(item);
        }
        public ActionResult ProductList()
        {
            var query = from items in db.Items
                        join categories in db.Categories on items.Category_idCategory equals categories.idCategory
                        select items;

            return View(query);
        }

        public ActionResult Favorites()
        {
            return View();
        }
    }
}