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
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Customer).Include(s => s.Employee).Include(s => s.ServiceState).Include(s => s.ServiceType);
            return View(services.ToList());
        }

        // GET: Services/Details/5
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name");
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name");
            ViewBag.ServiceState_idServiceState = new SelectList(db.ServiceStates, "idServiceState", "state");
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type");
            return View();
        }

        // POST: Services/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "idService,title,price,dateOfEmployment,description,dateOfCompletion,Customer_idCustomer,ServiceType_idServiceType,Employee_idEmployee,ServiceState_idServiceState")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", service.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceState_idServiceState = new SelectList(db.ServiceStates, "idServiceState", "state", service.ServiceState_idServiceState);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type", service.ServiceType_idServiceType);
            return View(service);
        }

        // GET: Services/Edit/5
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
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
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", service.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceState_idServiceState = new SelectList(db.ServiceStates, "idServiceState", "state", service.ServiceState_idServiceState);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type", service.ServiceType_idServiceType);
            return View(service);
        }

        // POST: Services/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult Edit([Bind(Include = "idService,title,price,dateOfEmployment,description,dateOfCompletion,Customer_idCustomer,ServiceType_idServiceType,Employee_idEmployee,ServiceState_idServiceState")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_idCustomer = new SelectList(db.Customers, "idCustomer", "name", service.Customer_idCustomer);
            ViewBag.Employee_idEmployee = new SelectList(db.Employees, "idEmployee", "name", service.Employee_idEmployee);
            ViewBag.ServiceState_idServiceState = new SelectList(db.ServiceStates, "idServiceState", "state", service.ServiceState_idServiceState);
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type", service.ServiceType_idServiceType);
            return View(service);
        }

        // GET: Services/Delete/5
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
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
        [Authorize(Roles = "Administrator, Pracownik serwisu")]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
