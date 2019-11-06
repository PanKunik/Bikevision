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
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: Items
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Brand).Include(i => i.Category).Include(i => i.ItemType);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create()
        {
            ViewBag.Brand_idBrand = new SelectList(db.Brands, "idBrand", "brand1");
            ViewBag.Category_idCategory = new SelectList(db.Categories, "idCategory", "category1");
            ViewBag.ItemType_idItemType = new SelectList(db.ItemTypes, "idItemType", "type");
            return View();
        }

        // POST: Items/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create([Bind(Include = "idItem,name,description,avability,price,discount,outlet,weight,dimensions,ItemType_idItemType,Category_idCategory,Brand_idBrand")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Brand_idBrand = new SelectList(db.Brands, "idBrand", "brand1", item.Brand_idBrand);
            ViewBag.Category_idCategory = new SelectList(db.Categories, "idCategory", "category1", item.Category_idCategory);
            ViewBag.ItemType_idItemType = new SelectList(db.ItemTypes, "idItemType", "type", item.ItemType_idItemType);
            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Brand_idBrand = new SelectList(db.Brands, "idBrand", "brand1", item.Brand_idBrand);
            ViewBag.Category_idCategory = new SelectList(db.Categories, "idCategory", "category1", item.Category_idCategory);
            ViewBag.ItemType_idItemType = new SelectList(db.ItemTypes, "idItemType", "type", item.ItemType_idItemType);
            return View(item);
        }

        // POST: Items/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit([Bind(Include = "idItem,name,description,avability,price,discount,outlet,weight,dimensions,ItemType_idItemType,Category_idCategory,Brand_idBrand")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Brand_idBrand = new SelectList(db.Brands, "idBrand", "brand1", item.Brand_idBrand);
            ViewBag.Category_idCategory = new SelectList(db.Categories, "idCategory", "category1", item.Category_idCategory);
            ViewBag.ItemType_idItemType = new SelectList(db.ItemTypes, "idItemType", "type", item.ItemType_idItemType);
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator, Moderator")]
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
