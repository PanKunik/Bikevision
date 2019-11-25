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
    public class FeatureValueOfItemsController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: FeatureValueOfItems
        [Authorize(Roles = "Administrator, Moderator, Pracownik sklepu")]
        public ActionResult Index()
        {
            var featureValueOfItems = db.FeatureValueOfItems.Include(f => f.Feature).Include(f => f.FeatureValue).Include(f => f.Item);
            return View(featureValueOfItems.ToList());
        }

        // GET: FeatureValueOfItems/Details/5
        [Authorize(Roles = "Administrator, Moderator, Pracownik sklepu")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureValueOfItem featureValueOfItem = db.FeatureValueOfItems.Find(id);
            if (featureValueOfItem == null)
            {
                return HttpNotFound();
            }
            return View(featureValueOfItem);
        }

        // GET: FeatureValueOfItems/Create
        [Authorize(Roles = "Administrator, Moderator, Pracownik sklepu")]
        public ActionResult Create()
        {
            ViewBag.Feature_idFeature1 = new SelectList(db.Features, "idFeature", "feature1");
            ViewBag.FeatureValue_idFeatureValue = new SelectList(db.FeatureValues, "idFeatureValue", "featureValue1");
            ViewBag.Item_idItem1 = new SelectList(db.Items, "idItem", "name");
            return View();
        }

        // POST: FeatureValueOfItems/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator, Pracownik sklepu")]
        public ActionResult Create([Bind(Include = "Feature_idFeature1,Item_idItem1,FeatureValue_idFeatureValue")] FeatureValueOfItem featureValueOfItem)
        {
            if (ModelState.IsValid)
            {
                db.FeatureValueOfItems.Add(featureValueOfItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Feature_idFeature1 = new SelectList(db.Features, "idFeature", "feature1", featureValueOfItem.Feature_idFeature1);
            ViewBag.FeatureValue_idFeatureValue = new SelectList(db.FeatureValues, "idFeatureValue", "featureValue1", featureValueOfItem.FeatureValue_idFeatureValue);
            ViewBag.Item_idItem1 = new SelectList(db.Items, "idItem", "name", featureValueOfItem.Item_idItem1);
            return View(featureValueOfItem);
        }

        // GET: FeatureValueOfItems/Edit/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureValueOfItem featureValueOfItem = db.FeatureValueOfItems.Find(id);
            if (featureValueOfItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Feature_idFeature1 = new SelectList(db.Features, "idFeature", "feature1", featureValueOfItem.Feature_idFeature1);
            ViewBag.FeatureValue_idFeatureValue = new SelectList(db.FeatureValues, "idFeatureValue", "featureValue1", featureValueOfItem.FeatureValue_idFeatureValue);
            ViewBag.Item_idItem1 = new SelectList(db.Items, "idItem", "name", featureValueOfItem.Item_idItem1);
            return View(featureValueOfItem);
        }

        // POST: FeatureValueOfItems/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit([Bind(Include = "Feature_idFeature1,Item_idItem1,FeatureValue_idFeatureValue")] FeatureValueOfItem featureValueOfItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(featureValueOfItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Feature_idFeature1 = new SelectList(db.Features, "idFeature", "feature1", featureValueOfItem.Feature_idFeature1);
            ViewBag.FeatureValue_idFeatureValue = new SelectList(db.FeatureValues, "idFeatureValue", "featureValue1", featureValueOfItem.FeatureValue_idFeatureValue);
            ViewBag.Item_idItem1 = new SelectList(db.Items, "idItem", "name", featureValueOfItem.Item_idItem1);
            return View(featureValueOfItem);
        }

        // GET: FeatureValueOfItems/Delete/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureValueOfItem featureValueOfItem = db.FeatureValueOfItems.Find(id);
            if (featureValueOfItem == null)
            {
                return HttpNotFound();
            }
            return View(featureValueOfItem);
        }

        // POST: FeatureValueOfItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            FeatureValueOfItem featureValueOfItem = db.FeatureValueOfItems.Find(id);
            db.FeatureValueOfItems.Remove(featureValueOfItem);
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
