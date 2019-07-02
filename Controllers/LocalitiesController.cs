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
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: Localities
        public ActionResult Index()
        {
            return View(db.Miejscowosc.ToList());
        }

        // GET: Localities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miejscowosc miejscowosc = db.Miejscowosc.Find(id);
            if (miejscowosc == null)
            {
                return HttpNotFound();
            }
            return View(miejscowosc);
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
        public ActionResult Create([Bind(Include = "idMiejscowosc,miejscowosc1")] Miejscowosc miejscowosc)
        {
            if (ModelState.IsValid)
            {
                db.Miejscowosc.Add(miejscowosc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(miejscowosc);
        }

        // GET: Localities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miejscowosc miejscowosc = db.Miejscowosc.Find(id);
            if (miejscowosc == null)
            {
                return HttpNotFound();
            }
            return View(miejscowosc);
        }

        // POST: Localities/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMiejscowosc,miejscowosc1")] Miejscowosc miejscowosc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(miejscowosc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(miejscowosc);
        }

        // GET: Localities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miejscowosc miejscowosc = db.Miejscowosc.Find(id);
            if (miejscowosc == null)
            {
                return HttpNotFound();
            }
            return View(miejscowosc);
        }

        // POST: Localities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Miejscowosc miejscowosc = db.Miejscowosc.Find(id);
            db.Miejscowosc.Remove(miejscowosc);
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
