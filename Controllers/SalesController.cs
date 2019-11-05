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
    public class SalesController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: Sales
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Customer).Include(s => s.Employee).Include(s => s.SaleType);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name");
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name");
            ViewBag.SaleType_idSaleType = new SelectList(db.SaleTypes, "idSaleType", "type");
            return View();
        }

        // POST: Sales/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "idSale,date,Customer_idCustomer,SaleType_idSaleType,Employee_idEmployee")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", sale.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", sale.Employee_idEmployee);
            ViewBag.SaleType_idSaleType = new SelectList(db.SaleTypes, "idSaleType", "type", sale.SaleType_idSaleType);
            return View(sale);
        }

        // GET: Sales/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", sale.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", sale.Employee_idEmployee);
            ViewBag.SaleType_idSaleType = new SelectList(db.SaleTypes, "idSaleType", "type", sale.SaleType_idSaleType);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "idSale,date,Customer_idCustomer,SaleType_idSaleType,Employee_idEmployee")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", sale.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", sale.Employee_idEmployee);
            ViewBag.SaleType_idSaleType = new SelectList(db.SaleTypes, "idSaleType", "type", sale.SaleType_idSaleType);
            return View(sale);
        }

        // GET: Sales/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
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
