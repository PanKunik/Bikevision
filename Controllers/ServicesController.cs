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
        private BikeVisionDBEntities2 db = new BikeVisionDBEntities2();

        // GET: Services
        public ActionResult Index()
        {
            var service = db.Service.Include(s => s.Customer).Include(s => s.Employee).Include(s => s.ServiceType);
            return View(service.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Service.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.Customer_idCustomer = new SelectList(db.Customer, "idCustomer", "name");
            ViewBag.Employee_idEmployee = new SelectList(db.Employee, "idEmployee", "name");
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceType, "idServiceType", "type");
            return View();
        }

        // POST: Services/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idService,title,price,dateOfEmployment,description,dateOfCompletion,Customer_idCustomer,ServiceType_idServiceType,Employee_idEmployee")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Service.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_idCustomer = new SelectList(db.Customer, "idCustomer", "name", service.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employee, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceType, "idServiceType", "type", service.ServiceType_idServiceType);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Service.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_idCustomer = new SelectList(db.Customer, "idCustomer", "name", service.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employee, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceType, "idServiceType", "type", service.ServiceType_idServiceType);
            return View(service);
        }

        // POST: Services/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idService,title,price,dateOfEmployment,description,dateOfCompletion,Customer_idCustomer,ServiceType_idServiceType,Employee_idEmployee")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_idCustomer = new SelectList(db.Customer, "idCustomer", "name", service.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employee, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceType, "idServiceType", "type", service.ServiceType_idServiceType);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Service.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Service.Find(id);
            db.Service.Remove(service);
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
