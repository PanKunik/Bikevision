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
    public class ObjectsController : Controller
    {
        private BikeVisionDBEntities db = new BikeVisionDBEntities();

        // GET: Objects
        public ActionResult Index()
        {
            var przedmiot = db.Przedmiot.Include(p => p.Kategoria).Include(p => p.TypPrzedmiotu);
            return View(przedmiot.ToList());
        }

        // GET: Objects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmiot.Find(id);
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            return View(przedmiot);
        }

        // GET: Objects/Create
        public ActionResult Create()
        {
            ViewBag.Kategoria_idKategoria = new SelectList(db.Kategoria, "idKategoria", "kategoria1");
            ViewBag.TypPrzedmiotu_idTypPrzedmiotu = new SelectList(db.TypPrzedmiotu, "idTypPrzedmiotu", "typ");
            return View();
        }

        // POST: Objects/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPrzedmiot,nazwaPrzedmiotu,opisPrzedmiotu,cena,waga,wymiary,TypPrzedmiotu_idTypPrzedmiotu,Kategoria_idKategoria")] Przedmiot przedmiot)
        {
            if (ModelState.IsValid)
            {
                db.Przedmiot.Add(przedmiot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Kategoria_idKategoria = new SelectList(db.Kategoria, "idKategoria", "kategoria1", przedmiot.Kategoria_idKategoria);
            ViewBag.TypPrzedmiotu_idTypPrzedmiotu = new SelectList(db.TypPrzedmiotu, "idTypPrzedmiotu", "typ", przedmiot.TypPrzedmiotu_idTypPrzedmiotu);
            return View(przedmiot);
        }

        // GET: Objects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmiot.Find(id);
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kategoria_idKategoria = new SelectList(db.Kategoria, "idKategoria", "kategoria1", przedmiot.Kategoria_idKategoria);
            ViewBag.TypPrzedmiotu_idTypPrzedmiotu = new SelectList(db.TypPrzedmiotu, "idTypPrzedmiotu", "typ", przedmiot.TypPrzedmiotu_idTypPrzedmiotu);
            return View(przedmiot);
        }

        // POST: Objects/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPrzedmiot,nazwaPrzedmiotu,opisPrzedmiotu,cena,waga,wymiary,TypPrzedmiotu_idTypPrzedmiotu,Kategoria_idKategoria")] Przedmiot przedmiot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przedmiot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Kategoria_idKategoria = new SelectList(db.Kategoria, "idKategoria", "kategoria1", przedmiot.Kategoria_idKategoria);
            ViewBag.TypPrzedmiotu_idTypPrzedmiotu = new SelectList(db.TypPrzedmiotu, "idTypPrzedmiotu", "typ", przedmiot.TypPrzedmiotu_idTypPrzedmiotu);
            return View(przedmiot);
        }

        // GET: Objects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmiot.Find(id);
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            return View(przedmiot);
        }

        // POST: Objects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Przedmiot przedmiot = db.Przedmiot.Find(id);
            db.Przedmiot.Remove(przedmiot);
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
