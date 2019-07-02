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
    public class ServicesController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: Services
        public ActionResult Index()
        {
            var serwis = db.Serwis.Include(s => s.Klient).Include(s => s.Pracownik).Include(s => s.TypSerwisu);
            return View(serwis.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serwis serwis = db.Serwis.Find(id);
            if (serwis == null)
            {
                return HttpNotFound();
            }
            return View(serwis);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.Klient_idKlient = new SelectList(db.Klient, "idKlient", "imie");
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie");
            ViewBag.TypSerwisu_idTypSerwisu = new SelectList(db.TypSerwisu, "idTypSerwisu", "typ");
            return View();
        }

        // POST: Services/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSerwis,tytul,cena,dataZlecenia,opis,dataWykonania,Klient_idKlient,Pracownik_idPracownik,TypSerwisu_idTypSerwisu")] Serwis serwis)
        {
            if (ModelState.IsValid)
            {
                db.Serwis.Add(serwis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Klient_idKlient = new SelectList(db.Klient, "idKlient", "imie", serwis.Klient_idKlient);
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", serwis.Pracownik_idPracownik);
            ViewBag.TypSerwisu_idTypSerwisu = new SelectList(db.TypSerwisu, "idTypSerwisu", "typ", serwis.TypSerwisu_idTypSerwisu);
            return View(serwis);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serwis serwis = db.Serwis.Find(id);
            if (serwis == null)
            {
                return HttpNotFound();
            }
            ViewBag.Klient_idKlient = new SelectList(db.Klient, "idKlient", "imie", serwis.Klient_idKlient);
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", serwis.Pracownik_idPracownik);
            ViewBag.TypSerwisu_idTypSerwisu = new SelectList(db.TypSerwisu, "idTypSerwisu", "typ", serwis.TypSerwisu_idTypSerwisu);
            return View(serwis);
        }

        // POST: Services/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSerwis,tytul,cena,dataZlecenia,opis,dataWykonania,Klient_idKlient,Pracownik_idPracownik,TypSerwisu_idTypSerwisu")] Serwis serwis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serwis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Klient_idKlient = new SelectList(db.Klient, "idKlient", "imie", serwis.Klient_idKlient);
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", serwis.Pracownik_idPracownik);
            ViewBag.TypSerwisu_idTypSerwisu = new SelectList(db.TypSerwisu, "idTypSerwisu", "typ", serwis.TypSerwisu_idTypSerwisu);
            return View(serwis);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serwis serwis = db.Serwis.Find(id);
            if (serwis == null)
            {
                return HttpNotFound();
            }
            return View(serwis);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Serwis serwis = db.Serwis.Find(id);
            db.Serwis.Remove(serwis);
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
