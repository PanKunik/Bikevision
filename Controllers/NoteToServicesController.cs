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
    public class NoteToServicesController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: NoteToServices
        [Authorize(Roles ="Administrator, Pracownik serwisu")]
        public ActionResult Index()
        {
            var noteToServices = db.NoteToServices.Include(n => n.AspNetUser).Include(n => n.Note).Include(n => n.Service);
            return View(noteToServices.ToList());
        }

        // GET: NoteToServices/Details/5
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteToService noteToService = db.NoteToServices.Find(id);
            if (noteToService == null)
            {
                return HttpNotFound();
            }
            return View(noteToService);
        }

        // GET: NoteToServices/Create
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult Create()
        {
            ViewBag.AspNetUser_idAspNetUser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Note_idNote = new SelectList(db.Notes, "idNote", "note1");
            ViewBag.Service_idService = new SelectList(db.Services, "idService", "title");
            return View();
        }

        // POST: NoteToServices/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult Create([Bind(Include = "Note_idNote,Service_idService,date,AspNetUser_idAspNetUser")] NoteToService noteToService)
        {
            if (ModelState.IsValid)
            {
                db.NoteToServices.Add(noteToService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUser_idAspNetUser = new SelectList(db.AspNetUsers, "Id", "Email", noteToService.AspNetUser_idAspNetUser);
            ViewBag.Note_idNote = new SelectList(db.Notes, "idNote", "note1", noteToService.Note_idNote);
            ViewBag.Service_idService = new SelectList(db.Services, "idService", "title", noteToService.Service_idService);
            return View(noteToService);
        }

        // GET: NoteToServices/Edit/5
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteToService noteToService = db.NoteToServices.Find(id);
            if (noteToService == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUser_idAspNetUser = new SelectList(db.AspNetUsers, "Id", "Email", noteToService.AspNetUser_idAspNetUser);
            ViewBag.Note_idNote = new SelectList(db.Notes, "idNote", "note1", noteToService.Note_idNote);
            ViewBag.Service_idService = new SelectList(db.Services, "idService", "title", noteToService.Service_idService);
            return View(noteToService);
        }

        // POST: NoteToServices/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult Edit([Bind(Include = "Note_idNote,Service_idService,date,AspNetUser_idAspNetUser")] NoteToService noteToService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noteToService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUser_idAspNetUser = new SelectList(db.AspNetUsers, "Id", "Email", noteToService.AspNetUser_idAspNetUser);
            ViewBag.Note_idNote = new SelectList(db.Notes, "idNote", "note1", noteToService.Note_idNote);
            ViewBag.Service_idService = new SelectList(db.Services, "idService", "title", noteToService.Service_idService);
            return View(noteToService);
        }

        // GET: NoteToServices/Delete/5
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteToService noteToService = db.NoteToServices.Find(id);
            if (noteToService == null)
            {
                return HttpNotFound();
            }
            return View(noteToService);
        }

        // POST: NoteToServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult DeleteConfirmed(int id)
        {
            NoteToService noteToService = db.NoteToServices.Find(id);
            db.NoteToServices.Remove(noteToService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator, Pracownik serwisu")]
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
