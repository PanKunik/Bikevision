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
    public class CustomersController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var klient = db.Klient.Include(k => k.Miejscowosc);
            return View(klient.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = db.Klient.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.Miejscowosc_idMiejscowosc = new SelectList(db.Miejscowosc, "idMiejscowosc", "miejscowosc1");
            return View();
        }

        // POST: Customers/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idKlient,imie,nazwisko,login,hash,numerTelefonu,adresEmail,adresZamieszkania,kodPocztowy,Miejscowosc_idMiejscowosc")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                db.Klient.Add(klient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Miejscowosc_idMiejscowosc = new SelectList(db.Miejscowosc, "idMiejscowosc", "miejscowosc1", klient.Miejscowosc_idMiejscowosc);
            return View(klient);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = db.Klient.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            ViewBag.Miejscowosc_idMiejscowosc = new SelectList(db.Miejscowosc, "idMiejscowosc", "miejscowosc1", klient.Miejscowosc_idMiejscowosc);
            return View(klient);
        }

        // POST: Customers/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idKlient,imie,nazwisko,login,hash,numerTelefonu,adresEmail,adresZamieszkania,kodPocztowy,Miejscowosc_idMiejscowosc")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Miejscowosc_idMiejscowosc = new SelectList(db.Miejscowosc, "idMiejscowosc", "miejscowosc1", klient.Miejscowosc_idMiejscowosc);
            return View(klient);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = db.Klient.Find(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klient klient = db.Klient.Find(id);
            db.Klient.Remove(klient);
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
