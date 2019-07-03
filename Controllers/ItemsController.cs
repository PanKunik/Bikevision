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
    public class ItemsController : Controller
    {
        private BikeVisionDBEntities2 db = new BikeVisionDBEntities2();

        // GET: Items
        public ActionResult Index()
        {
            var item = db.Item.Include(i => i.Category).Include(i => i.ItemType);
            return View(item.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.Category_idCategory = new SelectList(db.Category, "idCategory", "category1");
            ViewBag.ItemType_idItemType = new SelectList(db.ItemType, "idItemType", "type");
            return View();
        }

        // POST: Items/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idItem,itemName,itemDescription,price,weight,dimensions,ItemType_idItemType,Category_idCategory")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Item.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_idCategory = new SelectList(db.Category, "idCategory", "category1", item.Category_idCategory);
            ViewBag.ItemType_idItemType = new SelectList(db.ItemType, "idItemType", "type", item.ItemType_idItemType);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_idCategory = new SelectList(db.Category, "idCategory", "category1", item.Category_idCategory);
            ViewBag.ItemType_idItemType = new SelectList(db.ItemType, "idItemType", "type", item.ItemType_idItemType);
            return View(item);
        }

        // POST: Items/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idItem,itemName,itemDescription,price,weight,dimensions,ItemType_idItemType,Category_idCategory")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_idCategory = new SelectList(db.Category, "idCategory", "category1", item.Category_idCategory);
            ViewBag.ItemType_idItemType = new SelectList(db.ItemType, "idItemType", "type", item.ItemType_idItemType);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Item.Find(id);
            db.Item.Remove(item);
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
