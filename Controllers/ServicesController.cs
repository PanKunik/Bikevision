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
        private bikewayDBEntities db = new bikewayDBEntities();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Employee).Include(s => s.ServiceType).Include(s => s.Customer);
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name");
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type");
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name");
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
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type", service.ServiceType_idServiceType);
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", service.Customer_idCustomer);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type", service.ServiceType_idServiceType);
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", service.Customer_idCustomer);
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
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type", service.ServiceType_idServiceType);
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", service.Customer_idCustomer);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
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
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
