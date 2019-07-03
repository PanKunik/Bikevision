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
    public class LocalitiesController : Controller
    {
        private BikeVisionDBEntities2 db = new BikeVisionDBEntities2();

        // GET: Localities
        public ActionResult Index()
        {
            return View(db.Locality.ToList());
        }

        // GET: Localities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locality locality = db.Locality.Find(id);
            if (locality == null)
            {
                return HttpNotFound();
            }
            return View(locality);
        }

        // GET: Localities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Localities/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLocality,locality1")] Locality locality)
        {
            if (ModelState.IsValid)
            {
                db.Locality.Add(locality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locality);
        }

        // GET: Localities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locality locality = db.Locality.Find(id);
            if (locality == null)
            {
                return HttpNotFound();
            }
            return View(locality);
        }

        // POST: Localities/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLocality,locality1")] Locality locality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locality);
        }

        // GET: Localities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locality locality = db.Locality.Find(id);
            if (locality == null)
            {
                return HttpNotFound();
            }
            return View(locality);
        }

        // POST: Localities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locality locality = db.Locality.Find(id);
            db.Locality.Remove(locality);
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
