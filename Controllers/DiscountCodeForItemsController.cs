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
    public class DiscountCodeForItemsController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: DiscountCodeForItems
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Index()
        {
            var discountCodeForItems = db.DiscountCodeForItems.Include(d => d.DiscountCode).Include(d => d.Item);
            return View(discountCodeForItems.ToList());
        }

        // GET: DiscountCodeForItems/Details/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCodeForItem discountCodeForItem = db.DiscountCodeForItems.Find(id);
            if (discountCodeForItem == null)
            {
                return HttpNotFound();
            }
            return View(discountCodeForItem);
        }

        // GET: DiscountCodeForItems/Create
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create()
        {
            ViewBag.DiscountCode_idDiscountCode = new SelectList(db.DiscountCodes, "idDiscountCode", "code");
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "name");
            return View();
        }

        // POST: DiscountCodeForItems/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create([Bind(Include = "Item_idItem,DiscountCode_idDiscountCode,discount")] DiscountCodeForItem discountCodeForItem)
        {
            if (ModelState.IsValid)
            {
                db.DiscountCodeForItems.Add(discountCodeForItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiscountCode_idDiscountCode = new SelectList(db.DiscountCodes, "idDiscountCode", "code", discountCodeForItem.DiscountCode_idDiscountCode);
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "name", discountCodeForItem.Item_idItem);
            return View(discountCodeForItem);
        }

        // GET: DiscountCodeForItems/Edit/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCodeForItem discountCodeForItem = db.DiscountCodeForItems.Find(id);
            if (discountCodeForItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiscountCode_idDiscountCode = new SelectList(db.DiscountCodes, "idDiscountCode", "code", discountCodeForItem.DiscountCode_idDiscountCode);
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "name", discountCodeForItem.Item_idItem);
            return View(discountCodeForItem);
        }

        // POST: DiscountCodeForItems/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit([Bind(Include = "Item_idItem,DiscountCode_idDiscountCode,discount")] DiscountCodeForItem discountCodeForItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discountCodeForItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiscountCode_idDiscountCode = new SelectList(db.DiscountCodes, "idDiscountCode", "code", discountCodeForItem.DiscountCode_idDiscountCode);
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "name", discountCodeForItem.Item_idItem);
            return View(discountCodeForItem);
        }

        // GET: DiscountCodeForItems/Delete/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCodeForItem discountCodeForItem = db.DiscountCodeForItems.Find(id);
            if (discountCodeForItem == null)
            {
                return HttpNotFound();
            }
            return View(discountCodeForItem);
        }

        // POST: DiscountCodeForItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            DiscountCodeForItem discountCodeForItem = db.DiscountCodeForItems.Find(id);
            db.DiscountCodeForItems.Remove(discountCodeForItem);
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
