using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using bikevision.Models;

namespace bikevision.Controllers
{
    public class ShoppingCartController : Controller
    {
        bikewayDBEntities db = new bikewayDBEntities();
        private string sessionCartString = "Cart";

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Order()
        {
            
            return View();
        }

        // Post: ShoppingCart/Final
        [HttpPost]
        public ActionResult Final()
        {
            
            return View();
        }

        //Get: ShoppingCart/Order
        //[HttpGet]
        //public ActionResult Order()
        //{
        //    RedirectToAction("Shop", "Index");
        //    return View();
        //}

        //// Get: ShoppingCart/Final
        //[HttpGet]
        //public ActionResult Final()
        //{
        //    RedirectToAction("Shop", "Index");
        //    return View();
        //}

        public ActionResult OrderNow(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            if(Session[sessionCartString] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Items.Find(id), 1)
                };

                Session[sessionCartString] = lsCart;
            }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
                int indexOfItem = doseItemExistInCart(id);

                if (indexOfItem == -1)
                    lsCart.Add(new Cart(db.Items.Find(id), 1));
                else
                    lsCart[indexOfItem].Quantity++;

                Session[sessionCartString] = lsCart;
            }
            return RedirectToAction("Index");
        }

        private int doseItemExistInCart(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            for(int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].Item.idItem == id)
                    return i;
            }
            return -1;
        }

        public ActionResult AddQuantity(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            int indexOfItem = doseItemExistInCart(id);

            if (indexOfItem == -1)
                lsCart.Add(new Cart(db.Items.Find(id), 1));
            else
                lsCart[indexOfItem].Quantity++;

            Session[sessionCartString] = lsCart;

            return RedirectToAction("Index");
        }
        public ActionResult DecrementQuantity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            int indexOfItem = doseItemExistInCart(id);

            if (indexOfItem != -1)
            {
                lsCart[indexOfItem].Quantity--;
                if (lsCart[indexOfItem].Quantity == 0)
                    Delete(lsCart[indexOfItem].Item.idItem);
            }

            return RedirectToAction("Index", "Shop");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }
            
            int indexOfItem = doseItemExistInCart(id);
            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            lsCart.RemoveAt(indexOfItem);

            if (lsCart.Count == 0)
                Session[sessionCartString] = null;

            return RedirectToAction("Index");
        }
    }
}
