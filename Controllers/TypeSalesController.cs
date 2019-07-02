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
    public class TypeSalesController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: TypeSales
        public ActionResult Index()
        {
            return View(db.TypSprzedazy.ToList());
        }

        // GET: TypeSales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypSprzedazy typSprzedazy = db.TypSprzedazy.Find(id);
            if (typSprzedazy == null)
            {
                return HttpNotFound();
            }
            return View(typSprzedazy);
        }

        // GET: TypeSales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeSales/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTypSprzedazy,typ")] TypSprzedazy typSprzedazy)
        {
            if (ModelState.IsValid)
            {
                db.TypSprzedazy.Add(typSprzedazy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typSprzedazy);
        }

        // GET: TypeSales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypSprzedazy typSprzedazy = db.TypSprzedazy.Find(id);
            if (typSprzedazy == null)
            {
                return HttpNotFound();
            }
            return View(typSprzedazy);
        }

        // POST: TypeSales/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTypSprzedazy,typ")] TypSprzedazy typSprzedazy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typSprzedazy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typSprzedazy);
        }

        // GET: TypeSales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypSprzedazy typSprzedazy = db.TypSprzedazy.Find(id);
            if (typSprzedazy == null)
            {
                return HttpNotFound();
            }
            return View(typSprzedazy);
        }

        // POST: TypeSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypSprzedazy typSprzedazy = db.TypSprzedazy.Find(id);
            db.TypSprzedazy.Remove(typSprzedazy);
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
