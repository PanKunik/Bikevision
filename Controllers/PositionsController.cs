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
    public class PositionsController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: Positions
        public ActionResult Index()
        {
            return View(db.Stanowisko.ToList());
        }

        // GET: Positions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stanowisko stanowisko = db.Stanowisko.Find(id);
            if (stanowisko == null)
            {
                return HttpNotFound();
            }
            return View(stanowisko);
        }

        // GET: Positions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idStanowisko,nazwa")] Stanowisko stanowisko)
        {
            if (ModelState.IsValid)
            {
                db.Stanowisko.Add(stanowisko);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stanowisko);
        }

        // GET: Positions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stanowisko stanowisko = db.Stanowisko.Find(id);
            if (stanowisko == null)
            {
                return HttpNotFound();
            }
            return View(stanowisko);
        }

        // POST: Positions/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idStanowisko,nazwa")] Stanowisko stanowisko)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stanowisko).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stanowisko);
        }

        // GET: Positions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stanowisko stanowisko = db.Stanowisko.Find(id);
            if (stanowisko == null)
            {
                return HttpNotFound();
            }
            return View(stanowisko);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stanowisko stanowisko = db.Stanowisko.Find(id);
            db.Stanowisko.Remove(stanowisko);
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
