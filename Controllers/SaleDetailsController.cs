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
    public class SaleDetailsController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: SaleDetails
        public ActionResult Index()
        {
            var saleDetails = db.SaleDetails.Include(s => s.Item).Include(s => s.Sale);
            return View(saleDetails.ToList());
        }

        // GET: SaleDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleDetail saleDetail = db.SaleDetails.Find(id);
            if (saleDetail == null)
            {
                return HttpNotFound();
            }
            return View(saleDetail);
        }

        // GET: SaleDetails/Create
        public ActionResult Create()
        {
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "itemName");
            ViewBag.Sale_idSale = new SelectList(db.Sales, "idSale", "idSale");
            return View();
        }

        // POST: SaleDetails/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "value,Sale_idSale,Item_idItem")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                db.SaleDetails.Add(saleDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "itemName", saleDetail.Item_idItem);
            ViewBag.Sale_idSale = new SelectList(db.Sales, "idSale", "idSale", saleDetail.Sale_idSale);
            return View(saleDetail);
        }

        // GET: SaleDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleDetail saleDetail = db.SaleDetails.Find(id);
            if (saleDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "itemName", saleDetail.Item_idItem);
            ViewBag.Sale_idSale = new SelectList(db.Sales, "idSale", "idSale", saleDetail.Sale_idSale);
            return View(saleDetail);
        }

        // POST: SaleDetails/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "value,Sale_idSale,Item_idItem")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "itemName", saleDetail.Item_idItem);
            ViewBag.Sale_idSale = new SelectList(db.Sales, "idSale", "idSale", saleDetail.Sale_idSale);
            return View(saleDetail);
        }

        // GET: SaleDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleDetail saleDetail = db.SaleDetails.Find(id);
            if (saleDetail == null)
            {
                return HttpNotFound();
            }
            return View(saleDetail);
        }

        // POST: SaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleDetail saleDetail = db.SaleDetails.Find(id);
            db.SaleDetails.Remove(saleDetail);
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
