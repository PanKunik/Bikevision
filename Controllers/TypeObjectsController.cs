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
    public class TypeObjectsController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: TypeObjects
        public ActionResult Index()
        {
            return View(db.TypPrzedmiotu.ToList());
        }

        // GET: TypeObjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypPrzedmiotu typPrzedmiotu = db.TypPrzedmiotu.Find(id);
            if (typPrzedmiotu == null)
            {
                return HttpNotFound();
            }
            return View(typPrzedmiotu);
        }

        // GET: TypeObjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeObjects/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTypPrzedmiotu,typ")] TypPrzedmiotu typPrzedmiotu)
        {
            if (ModelState.IsValid)
            {
                db.TypPrzedmiotu.Add(typPrzedmiotu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typPrzedmiotu);
        }

        // GET: TypeObjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypPrzedmiotu typPrzedmiotu = db.TypPrzedmiotu.Find(id);
            if (typPrzedmiotu == null)
            {
                return HttpNotFound();
            }
            return View(typPrzedmiotu);
        }

        // POST: TypeObjects/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTypPrzedmiotu,typ")] TypPrzedmiotu typPrzedmiotu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typPrzedmiotu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typPrzedmiotu);
        }

        // GET: TypeObjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypPrzedmiotu typPrzedmiotu = db.TypPrzedmiotu.Find(id);
            if (typPrzedmiotu == null)
            {
                return HttpNotFound();
            }
            return View(typPrzedmiotu);
        }

        // POST: TypeObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypPrzedmiotu typPrzedmiotu = db.TypPrzedmiotu.Find(id);
            db.TypPrzedmiotu.Remove(typPrzedmiotu);
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
