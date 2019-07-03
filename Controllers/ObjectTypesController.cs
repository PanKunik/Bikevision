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
    public class ObjectTypesController : Controller
    {
        private BikeVisionDBEntities1 db = new BikeVisionDBEntities1();

        // GET: ObjectTypes
        public ActionResult Index()
        {
            return View(db.ObjectType.ToList());
        }

        // GET: ObjectTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectType objectType = db.ObjectType.Find(id);
            if (objectType == null)
            {
                return HttpNotFound();
            }
            return View(objectType);
        }

        // GET: ObjectTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObjectTypes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idObjectType,type")] ObjectType objectType)
        {
            if (ModelState.IsValid)
            {
                db.ObjectType.Add(objectType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objectType);
        }

        // GET: ObjectTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectType objectType = db.ObjectType.Find(id);
            if (objectType == null)
            {
                return HttpNotFound();
            }
            return View(objectType);
        }

        // POST: ObjectTypes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idObjectType,type")] ObjectType objectType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objectType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objectType);
        }

        // GET: ObjectTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectType objectType = db.ObjectType.Find(id);
            if (objectType == null)
            {
                return HttpNotFound();
            }
            return View(objectType);
        }

        // POST: ObjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObjectType objectType = db.ObjectType.Find(id);
            db.ObjectType.Remove(objectType);
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
