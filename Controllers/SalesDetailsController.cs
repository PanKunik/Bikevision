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
    public class SalesDetailsController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: SalesDetails
        public ActionResult Index()
        {
            var szczegolySprzedazy = db.SzczegolySprzedazy.Include(s => s.Przedmiot).Include(s => s.Sprzedaz);
            return View(szczegolySprzedazy.ToList());
        }

        // GET: SalesDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SzczegolySprzedazy szczegolySprzedazy = db.SzczegolySprzedazy.Find(id);
            if (szczegolySprzedazy == null)
            {
                return HttpNotFound();
            }
            return View(szczegolySprzedazy);
        }

        // GET: SalesDetails/Create
        public ActionResult Create()
        {
            ViewBag.Przedmiot_idPrzedmiot = new SelectList(db.Przedmiot, "idPrzedmiot", "nazwaPrzedmiotu");
            ViewBag.Sprzedaz_idSprzedaz = new SelectList(db.Sprzedaz, "idSprzedaz", "idSprzedaz");
            return View();
        }

        // POST: SalesDetails/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Przedmiot_idPrzedmiot,Sprzedaz_idSprzedaz,wartosc")] SzczegolySprzedazy szczegolySprzedazy)
        {
            if (ModelState.IsValid)
            {
                db.SzczegolySprzedazy.Add(szczegolySprzedazy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Przedmiot_idPrzedmiot = new SelectList(db.Przedmiot, "idPrzedmiot", "nazwaPrzedmiotu", szczegolySprzedazy.Przedmiot_idPrzedmiot);
            ViewBag.Sprzedaz_idSprzedaz = new SelectList(db.Sprzedaz, "idSprzedaz", "idSprzedaz", szczegolySprzedazy.Sprzedaz_idSprzedaz);
            return View(szczegolySprzedazy);
        }

        // GET: SalesDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SzczegolySprzedazy szczegolySprzedazy = db.SzczegolySprzedazy.Find(id);
            if (szczegolySprzedazy == null)
            {
                return HttpNotFound();
            }
            ViewBag.Przedmiot_idPrzedmiot = new SelectList(db.Przedmiot, "idPrzedmiot", "nazwaPrzedmiotu", szczegolySprzedazy.Przedmiot_idPrzedmiot);
            ViewBag.Sprzedaz_idSprzedaz = new SelectList(db.Sprzedaz, "idSprzedaz", "idSprzedaz", szczegolySprzedazy.Sprzedaz_idSprzedaz);
            return View(szczegolySprzedazy);
        }

        // POST: SalesDetails/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Przedmiot_idPrzedmiot,Sprzedaz_idSprzedaz,wartosc")] SzczegolySprzedazy szczegolySprzedazy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(szczegolySprzedazy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Przedmiot_idPrzedmiot = new SelectList(db.Przedmiot, "idPrzedmiot", "nazwaPrzedmiotu", szczegolySprzedazy.Przedmiot_idPrzedmiot);
            ViewBag.Sprzedaz_idSprzedaz = new SelectList(db.Sprzedaz, "idSprzedaz", "idSprzedaz", szczegolySprzedazy.Sprzedaz_idSprzedaz);
            return View(szczegolySprzedazy);
        }

        // GET: SalesDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SzczegolySprzedazy szczegolySprzedazy = db.SzczegolySprzedazy.Find(id);
            if (szczegolySprzedazy == null)
            {
                return HttpNotFound();
            }
            return View(szczegolySprzedazy);
        }

        // POST: SalesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SzczegolySprzedazy szczegolySprzedazy = db.SzczegolySprzedazy.Find(id);
            db.SzczegolySprzedazy.Remove(szczegolySprzedazy);
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
