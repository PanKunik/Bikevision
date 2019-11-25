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
    public class FeatureValuesController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: FeatureValues
        [Authorize(Roles = "Administrator, Moderator, Pracownik sklepu")]
        public ActionResult Index()
        {
            return View(db.FeatureValues.ToList());
        }

        // GET: FeatureValues/Details/5
        [Authorize(Roles = "Administrator, Moderator, Pracownik sklepu")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureValue featureValue = db.FeatureValues.Find(id);
            if (featureValue == null)
            {
                return HttpNotFound();
            }
            return View(featureValue);
        }

        // GET: FeatureValues/Create
        [Authorize(Roles = "Administrator, Moderator, Pracownik sklepu")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeatureValues/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator, Pracownik sklepu")]
        public ActionResult Create([Bind(Include = "idFeatureValue,featureValue1")] FeatureValue featureValue)
        {
            if (ModelState.IsValid)
            {
                db.FeatureValues.Add(featureValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(featureValue);
        }

        // GET: FeatureValues/Edit/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureValue featureValue = db.FeatureValues.Find(id);
            if (featureValue == null)
            {
                return HttpNotFound();
            }
            return View(featureValue);
        }

        // POST: FeatureValues/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit([Bind(Include = "idFeatureValue,featureValue1")] FeatureValue featureValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(featureValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(featureValue);
        }

        // GET: FeatureValues/Delete/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureValue featureValue = db.FeatureValues.Find(id);
            if (featureValue == null)
            {
                return HttpNotFound();
            }
            return View(featureValue);
        }

        // POST: FeatureValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            FeatureValue featureValue = db.FeatureValues.Find(id);
            db.FeatureValues.Remove(featureValue);
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
