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
    public class ObjectsController : Controller
    {
        private BikeVisionDBEntities1 db = new BikeVisionDBEntities1();

        // GET: Objects
        public ActionResult Index()
        {
            var object = db.Object.Include(@ => @.Category).Include(@ => @.ObjectType);
            return View(object.ToList());
        }

        // GET: Objects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Object @object = db.Object.Find(id);
            if (@object == null)
            {
                return HttpNotFound();
            }
            return View(@object);
        }

        // GET: Objects/Create
        public ActionResult Create()
        {
            ViewBag.Category_idCategory = new SelectList(db.Category, "idCategory", "category1");
            ViewBag.ObjectType_idObjectType = new SelectList(db.ObjectType, "idObjectType", "type");
            return View();
        }

        // POST: Objects/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idObject,objectName,objectDescription,price,weight,dimensions,ObjectType_idObjectType,Category_idCategory")] Object @object)
        {
            if (ModelState.IsValid)
            {
                db.Object.Add(@object);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_idCategory = new SelectList(db.Category, "idCategory", "category1", @object.Category_idCategory);
            ViewBag.ObjectType_idObjectType = new SelectList(db.ObjectType, "idObjectType", "type", @object.ObjectType_idObjectType);
            return View(@object);
        }

        // GET: Objects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Object @object = db.Object.Find(id);
            if (@object == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_idCategory = new SelectList(db.Category, "idCategory", "category1", @object.Category_idCategory);
            ViewBag.ObjectType_idObjectType = new SelectList(db.ObjectType, "idObjectType", "type", @object.ObjectType_idObjectType);
            return View(@object);
        }

        // POST: Objects/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idObject,objectName,objectDescription,price,weight,dimensions,ObjectType_idObjectType,Category_idCategory")] Object @object)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@object).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_idCategory = new SelectList(db.Category, "idCategory", "category1", @object.Category_idCategory);
            ViewBag.ObjectType_idObjectType = new SelectList(db.ObjectType, "idObjectType", "type", @object.ObjectType_idObjectType);
            return View(@object);
        }

        // GET: Objects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Object @object = db.Object.Find(id);
            if (@object == null)
            {
                return HttpNotFound();
            }
            return View(@object);
        }

        // POST: Objects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Object @object = db.Object.Find(id);
            db.Object.Remove(@object);
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
