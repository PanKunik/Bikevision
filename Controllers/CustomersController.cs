using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bikevision.Models;

namespace bikevision.Controllers
{
    public class CustomersController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.AspNetUser).Include(c => c.Locality).Include(c => c.PermanentDiscount);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUsers_idAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1");
            ViewBag.PermanentDiscount_idPermanentDiscount = new SelectList(db.PermanentDiscounts, "idPermanentDiscount", "idPermanentDiscount");
            return View();
        }

        // POST: Customers/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCustomer,name,surname,telephoneNumber,emailAddress,addressOfResidence,zipCode,AspNetUsers_idAspNetUsers,Locality_idLocality,PermanentDiscount_idPermanentDiscount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUsers_idAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", customer.AspNetUsers_idAspNetUsers);
            ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);
            ViewBag.PermanentDiscount_idPermanentDiscount = new SelectList(db.PermanentDiscounts, "idPermanentDiscount", "idPermanentDiscount", customer.PermanentDiscount_idPermanentDiscount);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUsers_idAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", customer.AspNetUsers_idAspNetUsers);
            ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);
            ViewBag.PermanentDiscount_idPermanentDiscount = new SelectList(db.PermanentDiscounts, "idPermanentDiscount", "idPermanentDiscount", customer.PermanentDiscount_idPermanentDiscount);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCustomer,name,surname,telephoneNumber,emailAddress,addressOfResidence,zipCode,AspNetUsers_idAspNetUsers,Locality_idLocality,PermanentDiscount_idPermanentDiscount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUsers_idAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", customer.AspNetUsers_idAspNetUsers);
            ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);
            ViewBag.PermanentDiscount_idPermanentDiscount = new SelectList(db.PermanentDiscounts, "idPermanentDiscount", "idPermanentDiscount", customer.PermanentDiscount_idPermanentDiscount);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
