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
    public class SalesController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var sprzedaz = db.Sprzedaz.Include(s => s.Klient).Include(s => s.Pracownik).Include(s => s.TypSprzedazy);
            return View(sprzedaz.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprzedaz sprzedaz = db.Sprzedaz.Find(id);
            if (sprzedaz == null)
            {
                return HttpNotFound();
            }
            return View(sprzedaz);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.Klient_idKlient = new SelectList(db.Klient, "idKlient", "imie");
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie");
            ViewBag.TypSprzedazy_idTypSprzedazy = new SelectList(db.TypSprzedazy, "idTypSprzedazy", "typ");
            return View();
        }

        // POST: Sales/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSprzedaz,data,wartosc,Pracownik_idPracownik,Klient_idKlient,TypSprzedazy_idTypSprzedazy")] Sprzedaz sprzedaz)
        {
            if (ModelState.IsValid)
            {
                db.Sprzedaz.Add(sprzedaz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Klient_idKlient = new SelectList(db.Klient, "idKlient", "imie", sprzedaz.Klient_idKlient);
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", sprzedaz.Pracownik_idPracownik);
            ViewBag.TypSprzedazy_idTypSprzedazy = new SelectList(db.TypSprzedazy, "idTypSprzedazy", "typ", sprzedaz.TypSprzedazy_idTypSprzedazy);
            return View(sprzedaz);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprzedaz sprzedaz = db.Sprzedaz.Find(id);
            if (sprzedaz == null)
            {
                return HttpNotFound();
            }
            ViewBag.Klient_idKlient = new SelectList(db.Klient, "idKlient", "imie", sprzedaz.Klient_idKlient);
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", sprzedaz.Pracownik_idPracownik);
            ViewBag.TypSprzedazy_idTypSprzedazy = new SelectList(db.TypSprzedazy, "idTypSprzedazy", "typ", sprzedaz.TypSprzedazy_idTypSprzedazy);
            return View(sprzedaz);
        }

        // POST: Sales/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSprzedaz,data,wartosc,Pracownik_idPracownik,Klient_idKlient,TypSprzedazy_idTypSprzedazy")] Sprzedaz sprzedaz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprzedaz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Klient_idKlient = new SelectList(db.Klient, "idKlient", "imie", sprzedaz.Klient_idKlient);
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", sprzedaz.Pracownik_idPracownik);
            ViewBag.TypSprzedazy_idTypSprzedazy = new SelectList(db.TypSprzedazy, "idTypSprzedazy", "typ", sprzedaz.TypSprzedazy_idTypSprzedazy);
            return View(sprzedaz);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprzedaz sprzedaz = db.Sprzedaz.Find(id);
            if (sprzedaz == null)
            {
                return HttpNotFound();
            }
            return View(sprzedaz);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sprzedaz sprzedaz = db.Sprzedaz.Find(id);
            db.Sprzedaz.Remove(sprzedaz);
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
