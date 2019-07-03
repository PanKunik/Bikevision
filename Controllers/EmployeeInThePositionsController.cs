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
    public class EmployeeInThePositionsController : Controller
    {
        private BikeVisionDBEntities1 db = new BikeVisionDBEntities1();

        // GET: EmployeeInThePositions
        public ActionResult Index()
        {
            var employeeInThePosition = db.EmployeeInThePosition.Include(e => e.Employee).Include(e => e.Position);
            return View(employeeInThePosition.ToList());
        }

        // GET: EmployeeInThePositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInThePosition employeeInThePosition = db.EmployeeInThePosition.Find(id);
            if (employeeInThePosition == null)
            {
                return HttpNotFound();
            }
            return View(employeeInThePosition);
        }

        // GET: EmployeeInThePositions/Create
        public ActionResult Create()
        {
            ViewBag.Employee_idEmployee = new SelectList(db.Employee, "idEmployee", "name");
            ViewBag.Position_idPosition = new SelectList(db.Position, "idPosition", "name");
            return View();
        }

        // POST: EmployeeInThePositions/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dateOfEmployment,Employee_idEmployee,Position_idPosition")] EmployeeInThePosition employeeInThePosition)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeInThePosition.Add(employeeInThePosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_idEmployee = new SelectList(db.Employee, "idEmployee", "name", employeeInThePosition.Employee_idEmployee);
            ViewBag.Position_idPosition = new SelectList(db.Position, "idPosition", "name", employeeInThePosition.Position_idPosition);
            return View(employeeInThePosition);
        }

        // GET: EmployeeInThePositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInThePosition employeeInThePosition = db.EmployeeInThePosition.Find(id);
            if (employeeInThePosition == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_idEmployee = new SelectList(db.Employee, "idEmployee", "name", employeeInThePosition.Employee_idEmployee);
            ViewBag.Position_idPosition = new SelectList(db.Position, "idPosition", "name", employeeInThePosition.Position_idPosition);
            return View(employeeInThePosition);
        }

        // POST: EmployeeInThePositions/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dateOfEmployment,Employee_idEmployee,Position_idPosition")] EmployeeInThePosition employeeInThePosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeInThePosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_idEmployee = new SelectList(db.Employee, "idEmployee", "name", employeeInThePosition.Employee_idEmployee);
            ViewBag.Position_idPosition = new SelectList(db.Position, "idPosition", "name", employeeInThePosition.Position_idPosition);
            return View(employeeInThePosition);
        }

        // GET: EmployeeInThePositions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInThePosition employeeInThePosition = db.EmployeeInThePosition.Find(id);
            if (employeeInThePosition == null)
            {
                return HttpNotFound();
            }
            return View(employeeInThePosition);
        }

        // POST: EmployeeInThePositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeInThePosition employeeInThePosition = db.EmployeeInThePosition.Find(id);
            db.EmployeeInThePosition.Remove(employeeInThePosition);
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
