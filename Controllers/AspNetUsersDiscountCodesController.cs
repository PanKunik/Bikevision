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
    public class AspNetUsersDiscountCodesController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: AspNetUsersDiscountCodes
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var aspNetUsersDiscountCodes = db.AspNetUsersDiscountCodes.Include(a => a.AspNetUser).Include(a => a.DiscountCode);
            return View(aspNetUsersDiscountCodes.ToList());
        }

        // GET: AspNetUsersDiscountCodes/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsersDiscountCode aspNetUsersDiscountCode = db.AspNetUsersDiscountCodes.Find(id);
            if (aspNetUsersDiscountCode == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsersDiscountCode);
        }

        // GET: AspNetUsersDiscountCodes/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.AspNetUser_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.DiscountCode_idDiscountCode = new SelectList(db.DiscountCodes, "idDiscountCode", "code");
            return View();
        }

        // POST: AspNetUsersDiscountCodes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "AspNetUser_Id,DiscountCode_idDiscountCode,numberOfUses")] AspNetUsersDiscountCode aspNetUsersDiscountCode)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsersDiscountCodes.Add(aspNetUsersDiscountCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUser_Id = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUsersDiscountCode.AspNetUser_Id);
            ViewBag.DiscountCode_idDiscountCode = new SelectList(db.DiscountCodes, "idDiscountCode", "code", aspNetUsersDiscountCode.DiscountCode_idDiscountCode);
            return View(aspNetUsersDiscountCode);
        }

        // GET: AspNetUsersDiscountCodes/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsersDiscountCode aspNetUsersDiscountCode = db.AspNetUsersDiscountCodes.Find(id);
            if (aspNetUsersDiscountCode == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUser_Id = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUsersDiscountCode.AspNetUser_Id);
            ViewBag.DiscountCode_idDiscountCode = new SelectList(db.DiscountCodes, "idDiscountCode", "code", aspNetUsersDiscountCode.DiscountCode_idDiscountCode);
            return View(aspNetUsersDiscountCode);
        }

        // POST: AspNetUsersDiscountCodes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "AspNetUser_Id,DiscountCode_idDiscountCode,numberOfUses")] AspNetUsersDiscountCode aspNetUsersDiscountCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsersDiscountCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUser_Id = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUsersDiscountCode.AspNetUser_Id);
            ViewBag.DiscountCode_idDiscountCode = new SelectList(db.DiscountCodes, "idDiscountCode", "code", aspNetUsersDiscountCode.DiscountCode_idDiscountCode);
            return View(aspNetUsersDiscountCode);
        }

        // GET: AspNetUsersDiscountCodes/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsersDiscountCode aspNetUsersDiscountCode = db.AspNetUsersDiscountCodes.Find(id);
            if (aspNetUsersDiscountCode == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsersDiscountCode);
        }

        // POST: AspNetUsersDiscountCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsersDiscountCode aspNetUsersDiscountCode = db.AspNetUsersDiscountCodes.Find(id);
            db.AspNetUsersDiscountCodes.Remove(aspNetUsersDiscountCode);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
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
