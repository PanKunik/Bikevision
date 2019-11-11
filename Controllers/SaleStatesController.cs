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
    public class SaleStatesController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: SaleStates
        public ActionResult Index()
        {
            return View(db.SaleStates.ToList());
        }

        // GET: SaleStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleState saleState = db.SaleStates.Find(id);
            if (saleState == null)
            {
                return HttpNotFound();
            }
            return View(saleState);
        }

        // GET: SaleStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleStates/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSaleState,state")] SaleState saleState)
        {
            if (ModelState.IsValid)
            {
                db.SaleStates.Add(saleState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleState);
        }

        // GET: SaleStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleState saleState = db.SaleStates.Find(id);
            if (saleState == null)
            {
                return HttpNotFound();
            }
            return View(saleState);
        }

        // POST: SaleStates/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSaleState,state")] SaleState saleState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saleState);
        }

        // GET: SaleStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleState saleState = db.SaleStates.Find(id);
            if (saleState == null)
            {
                return HttpNotFound();
            }
            return View(saleState);
        }

        // POST: SaleStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleState saleState = db.SaleStates.Find(id);
            db.SaleStates.Remove(saleState);
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
