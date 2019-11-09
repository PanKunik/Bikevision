using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using bikevision.Models;
using System.Data.Entity;
using System.Threading.Tasks;

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
        public ActionResult Index(int? id)
        {
            // NoError -> move to next step of ordering
            if (Session[sessionCartString] != null)
                return RedirectToAction("Order");
            else
                return View();
        }

        public ActionResult Order()
        {
            if (Session[sessionCartString] != null)
            {
                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1");

                if (User.Identity.IsAuthenticated)
                {
                    IQueryable<Customer> existingCustomer = db.Customers.Where(cust => cust.AspNetUser.UserName == User.Identity.Name);

                    if (existingCustomer.Count() > 0)
                        return View(existingCustomer.First());
                    else
                        return View();
                }
                else
                    return View();
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order([Bind(Include = "name,surname,telephoneNumber,emailAddress,addressOfResidence,zipCode,Locality_idLocality")] Customer customer)
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();

                    Sale sale = new Sale();

                    sale.Customer_idCustomer = customer.idCustomer;
                    sale.date = DateTime.Now.Date;

                    SaleType saleTypeId = db.SaleTypes.Where(i => i.type == "Internetowa").First();
                    sale.SaleType_idSaleType = saleTypeId.idSaleType;

                    Employee employeeId = db.Employees.Where(i => i.name == "Internetowy").First();
                    sale.Employee_idEmployee = employeeId.idEmployee;

                    db.Sales.Add(sale);
                    db.SaveChanges();

                    List<Cart> lsCart = new List<Cart>();
                    lsCart = (List<Cart>)Session[sessionCartString];

                    int lastSaleDetails = 0;

                    foreach (var item in lsCart)
                    {
                        SaleDetail detailOfSale = new SaleDetail();

                        detailOfSale.Item_idItem = item.Item.idItem;
                        detailOfSale.Sale_idSale = db.Entry(sale).Entity.idSale;
                        lastSaleDetails = detailOfSale.Sale_idSale;
                        detailOfSale.value = item.Item.price * item.Quantity;

                        db.SaleDetails.Add(detailOfSale);
                        db.SaveChanges();

                    }

                    Session[sessionCartString] = null;

                    return RedirectToAction("Final", new { idOfSale = lastSaleDetails });
                }

                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);

                return View(customer);
            }
            else
            {
                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);

                if (ModelState.IsValid)
                {
                    IQueryable<Customer> Customers = db.Customers.Where(cust => cust.AspNetUser.UserName == User.Identity.Name);

                    if(Customers.Count() > 0)
                    {
                        customer = Customers.First();
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        customer.AspNetUsers_idAspNetUsers = db.AspNetUsers.Where(user => user.UserName == User.Identity.Name).First().Id;
                        db.Customers.Add(customer);
                        db.SaveChanges();
                    }

                    Sale sale = new Sale();

                    sale.Customer_idCustomer = customer.idCustomer;
                    sale.date = DateTime.Now.Date;

                    SaleType saleTypeId = db.SaleTypes.Where(i => i.type == "Internetowa").First();
                    sale.SaleType_idSaleType = saleTypeId.idSaleType;

                    Employee employeeId = db.Employees.Where(i => i.name == "Internetowy").First();
                    sale.Employee_idEmployee = employeeId.idEmployee;

                    db.Sales.Add(sale);
                    db.SaveChanges();

                    List<Cart> lsCart = new List<Cart>();
                    lsCart = (List<Cart>)Session[sessionCartString];

                    int lastSaleDetails = 0;

                    foreach (var item in lsCart)
                    {
                        SaleDetail detailOfSale = new SaleDetail();

                        detailOfSale.Item_idItem = item.Item.idItem;
                        detailOfSale.Sale_idSale = db.Entry(sale).Entity.idSale;
                        lastSaleDetails = detailOfSale.Sale_idSale;
                        detailOfSale.value = item.Item.price * item.Quantity;

                        db.SaleDetails.Add(detailOfSale);
                        db.SaveChanges();

                    }

                    Session[sessionCartString] = null;

                    return RedirectToAction("Final", new { idOfSale = lastSaleDetails });
                }

                return View();
            }
        }

        // GET: ShoppingCart/Final
        public ActionResult Final(int? idOfSale)
        {
            if (idOfSale != null)
            {
                //IQueryable details = new IQueryable();

                IQueryable<SaleDetail> details = db.SaleDetails.Where(i => i.Sale_idSale == idOfSale);

                List<SaleDetail> saleDetail = details.ToList();

                decimal[] sum = new decimal[saleDetail.Count];
                int index = 0;

                foreach (var quantity in saleDetail)
                {
                    sum[index] = quantity.value / quantity.Item.price;
                    index++;
                }
                ViewBag.values = sum;

                return View(saleDetail);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: ShoppingCart/Final
        [HttpPost]
        public ActionResult Final()
        {
            return RedirectToAction("Index", "Shop");
        }

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
            for (int i = 0; i < lsCart.Count; i++)
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
                return RedirectToAction("Index", "Shop");
            }

            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            int indexOfItem = doseItemExistInCart(id);

            if (indexOfItem != -1)
            {
                lsCart[indexOfItem].Quantity--;
                if (lsCart[indexOfItem].Quantity == 0)
                    Delete(lsCart[indexOfItem].Item.idItem);
            }

            return RedirectToAction("Index");
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
