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
    public class AspNetUserFavoritesController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: AspNetUserFavorites
        public ActionResult Index()
        {
            var aspNetUserFavorites = db.AspNetUserFavorites.Include(a => a.AspNetUser).Include(a => a.Item);
            return View(aspNetUserFavorites.ToList());
        }

        // GET: AspNetUserFavorites/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserFavorite aspNetUserFavorite = db.AspNetUserFavorites.Find(id);
            if (aspNetUserFavorite == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserFavorite);
        }

        // GET: AspNetUserFavorites/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUsers_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "name");
            return View();
        }

        // POST: AspNetUserFavorites/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AspNetUsers_Id,Item_idItem,dateOfCreation")] AspNetUserFavorite aspNetUserFavorite)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUserFavorites.Add(aspNetUserFavorite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUsers_Id = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserFavorite.AspNetUsers_Id);
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "name", aspNetUserFavorite.Item_idItem);
            return View(aspNetUserFavorite);
        }

        // GET: AspNetUserFavorites/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserFavorite aspNetUserFavorite = db.AspNetUserFavorites.Find(id);
            if (aspNetUserFavorite == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUsers_Id = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserFavorite.AspNetUsers_Id);
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "name", aspNetUserFavorite.Item_idItem);
            return View(aspNetUserFavorite);
        }

        // POST: AspNetUserFavorites/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AspNetUsers_Id,Item_idItem,dateOfCreation")] AspNetUserFavorite aspNetUserFavorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserFavorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUsers_Id = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserFavorite.AspNetUsers_Id);
            ViewBag.Item_idItem = new SelectList(db.Items, "idItem", "name", aspNetUserFavorite.Item_idItem);
            return View(aspNetUserFavorite);
        }

        // GET: AspNetUserFavorites/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserFavorite aspNetUserFavorite = db.AspNetUserFavorites.Find(id);
            if (aspNetUserFavorite == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserFavorite);
        }

        // POST: AspNetUserFavorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUserFavorite aspNetUserFavorite = db.AspNetUserFavorites.Find(id);
            db.AspNetUserFavorites.Remove(aspNetUserFavorite);
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
