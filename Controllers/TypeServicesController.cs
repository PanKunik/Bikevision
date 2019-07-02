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
    public class TypeServicesController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: TypeServices
        public ActionResult Index()
        {
            return View(db.TypSerwisu.ToList());
        }

        // GET: TypeServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypSerwisu typSerwisu = db.TypSerwisu.Find(id);
            if (typSerwisu == null)
            {
                return HttpNotFound();
            }
            return View(typSerwisu);
        }

        // GET: TypeServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeServices/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTypSerwisu,typ")] TypSerwisu typSerwisu)
        {
            if (ModelState.IsValid)
            {
                db.TypSerwisu.Add(typSerwisu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typSerwisu);
        }

        // GET: TypeServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypSerwisu typSerwisu = db.TypSerwisu.Find(id);
            if (typSerwisu == null)
            {
                return HttpNotFound();
            }
            return View(typSerwisu);
        }

        // POST: TypeServices/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTypSerwisu,typ")] TypSerwisu typSerwisu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typSerwisu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typSerwisu);
        }

        // GET: TypeServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypSerwisu typSerwisu = db.TypSerwisu.Find(id);
            if (typSerwisu == null)
            {
                return HttpNotFound();
            }
            return View(typSerwisu);
        }

        // POST: TypeServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypSerwisu typSerwisu = db.TypSerwisu.Find(id);
            db.TypSerwisu.Remove(typSerwisu);
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
