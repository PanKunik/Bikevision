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
    public class WorkerInThePositionsController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: WorkerInThePositions
        public ActionResult Index()
        {
            var pracownikNaStanowisku = db.PracownikNaStanowisku.Include(p => p.Pracownik).Include(p => p.Stanowisko);
            return View(pracownikNaStanowisku.ToList());
        }

        // GET: WorkerInThePositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracownikNaStanowisku pracownikNaStanowisku = db.PracownikNaStanowisku.Find(id);
            if (pracownikNaStanowisku == null)
            {
                return HttpNotFound();
            }
            return View(pracownikNaStanowisku);
        }

        // GET: WorkerInThePositions/Create
        public ActionResult Create()
        {
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie");
            ViewBag.Stanowisko_idStanowisko = new SelectList(db.Stanowisko, "idStanowisko", "nazwa");
            return View();
        }

        // POST: WorkerInThePositions/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pracownik_idPracownik,Stanowisko_idStanowisko,dataZatrudnienia")] PracownikNaStanowisku pracownikNaStanowisku)
        {
            if (ModelState.IsValid)
            {
                db.PracownikNaStanowisku.Add(pracownikNaStanowisku);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", pracownikNaStanowisku.Pracownik_idPracownik);
            ViewBag.Stanowisko_idStanowisko = new SelectList(db.Stanowisko, "idStanowisko", "nazwa", pracownikNaStanowisku.Stanowisko_idStanowisko);
            return View(pracownikNaStanowisku);
        }

        // GET: WorkerInThePositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracownikNaStanowisku pracownikNaStanowisku = db.PracownikNaStanowisku.Find(id);
            if (pracownikNaStanowisku == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", pracownikNaStanowisku.Pracownik_idPracownik);
            ViewBag.Stanowisko_idStanowisko = new SelectList(db.Stanowisko, "idStanowisko", "nazwa", pracownikNaStanowisku.Stanowisko_idStanowisko);
            return View(pracownikNaStanowisku);
        }

        // POST: WorkerInThePositions/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pracownik_idPracownik,Stanowisko_idStanowisko,dataZatrudnienia")] PracownikNaStanowisku pracownikNaStanowisku)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pracownikNaStanowisku).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pracownik_idPracownik = new SelectList(db.Pracownik, "idPracownik", "imie", pracownikNaStanowisku.Pracownik_idPracownik);
            ViewBag.Stanowisko_idStanowisko = new SelectList(db.Stanowisko, "idStanowisko", "nazwa", pracownikNaStanowisku.Stanowisko_idStanowisko);
            return View(pracownikNaStanowisku);
        }

        // GET: WorkerInThePositions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracownikNaStanowisku pracownikNaStanowisku = db.PracownikNaStanowisku.Find(id);
            if (pracownikNaStanowisku == null)
            {
                return HttpNotFound();
            }
            return View(pracownikNaStanowisku);
        }

        // POST: WorkerInThePositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PracownikNaStanowisku pracownikNaStanowisku = db.PracownikNaStanowisku.Find(id);
            db.PracownikNaStanowisku.Remove(pracownikNaStanowisku);
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
