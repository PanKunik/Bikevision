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
    public class SaleTypesController : Controller
    {
        private BikeVisionDBEntities2 db = new BikeVisionDBEntities2();

        // GET: SaleTypes
        public ActionResult Index()
        {
            return View(db.SaleType.ToList());
        }

        // GET: SaleTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleType saleType = db.SaleType.Find(id);
            if (saleType == null)
            {
                return HttpNotFound();
            }
            return View(saleType);
        }

        // GET: SaleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleTypes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSaleType,type")] SaleType saleType)
        {
            if (ModelState.IsValid)
            {
                db.SaleType.Add(saleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleType);
        }

        // GET: SaleTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleType saleType = db.SaleType.Find(id);
            if (saleType == null)
            {
                return HttpNotFound();
            }
            return View(saleType);
        }

        // POST: SaleTypes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSaleType,type")] SaleType saleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saleType);
        }

        // GET: SaleTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleType saleType = db.SaleType.Find(id);
            if (saleType == null)
            {
                return HttpNotFound();
            }
            return View(saleType);
        }

        // POST: SaleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleType saleType = db.SaleType.Find(id);
            db.SaleType.Remove(saleType);
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
