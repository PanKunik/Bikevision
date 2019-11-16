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
    public class PermanentDiscountsController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: PermanentDiscounts
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.PermanentDiscounts.ToList());
        }

        // GET: PermanentDiscounts/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermanentDiscount permanentDiscount = db.PermanentDiscounts.Find(id);
            if (permanentDiscount == null)
            {
                return HttpNotFound();
            }
            return View(permanentDiscount);
        }

        // GET: PermanentDiscounts/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PermanentDiscounts/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "idPermanentDiscount,discount,treshold")] PermanentDiscount permanentDiscount)
        {
            if (ModelState.IsValid)
            {
                db.PermanentDiscounts.Add(permanentDiscount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permanentDiscount);
        }

        // GET: PermanentDiscounts/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermanentDiscount permanentDiscount = db.PermanentDiscounts.Find(id);
            if (permanentDiscount == null)
            {
                return HttpNotFound();
            }
            return View(permanentDiscount);
        }

        // POST: PermanentDiscounts/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "idPermanentDiscount,discount,treshold")] PermanentDiscount permanentDiscount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permanentDiscount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permanentDiscount);
        }

        // GET: PermanentDiscounts/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermanentDiscount permanentDiscount = db.PermanentDiscounts.Find(id);
            if (permanentDiscount == null)
            {
                return HttpNotFound();
            }
            return View(permanentDiscount);
        }

        // POST: PermanentDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            PermanentDiscount permanentDiscount = db.PermanentDiscounts.Find(id);
            db.PermanentDiscounts.Remove(permanentDiscount);
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
