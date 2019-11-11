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
    public class ServiceStatesController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: ServiceStates
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.ServiceStates.ToList());
        }

        // GET: ServiceStates/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceState serviceState = db.ServiceStates.Find(id);
            if (serviceState == null)
            {
                return HttpNotFound();
            }
            return View(serviceState);
        }

        // GET: ServiceStates/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceStates/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "idServiceState,state")] ServiceState serviceState)
        {
            if (ModelState.IsValid)
            {
                db.ServiceStates.Add(serviceState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceState);
        }

        // GET: ServiceStates/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceState serviceState = db.ServiceStates.Find(id);
            if (serviceState == null)
            {
                return HttpNotFound();
            }
            return View(serviceState);
        }

        // POST: ServiceStates/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "idServiceState,state")] ServiceState serviceState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceState);
        }

        // GET: ServiceStates/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceState serviceState = db.ServiceStates.Find(id);
            if (serviceState == null)
            {
                return HttpNotFound();
            }
            return View(serviceState);
        }

        // POST: ServiceStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceState serviceState = db.ServiceStates.Find(id);
            db.ServiceStates.Remove(serviceState);
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
