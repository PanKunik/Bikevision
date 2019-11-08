using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bikevision.Models;

namespace bikevision.Controllers
{
    public class ServiceController : Controller
    {
        bikewayDBEntities db = new bikewayDBEntities();
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
        // GET: Service/NewServiceOrder
        public ActionResult NewServiceOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type");
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult NewServiceOrder(Service service)
        {
            ViewBag.ServiceType_idServiceType = new SelectList(db.ServiceTypes, "idServiceType", "type", service.ServiceType_idServiceType);

            if (ModelState.IsValid)
            {
                Employee employeeId = db.Employees.Where(i => i.name == "Internetowy").First();
                service.Employee_idEmployee = employeeId.idEmployee;

                service.dateOfEmployment = DateTime.Now;

                service.price = 1;

                IQueryable<Customer> customer = db.Customers.Where(c => c.AspNetUser.UserName == User.Identity.Name);

                if(customer.Count() > 0)
                {
                    service.Customer_idCustomer = customer.First().idCustomer;

                    db.Services.Add(service);
                    db.SaveChanges();

                    return RedirectToAction("ServiceOrders", "Account");
                }
                else
                {
                    Session["service"] = service;

                    return RedirectToAction("AddPersonalData");
                }
            }

            return View(service);
        }

        public ActionResult AddPersonalData()
        {
            if(User.Identity.IsAuthenticated)
            {
                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1");
                return View();
            }
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPersonalData([Bind(Include = "name,surname,telephoneNumber,emailAddress,addressOfResidence,zipCode,AspNetUsers_idAspNetUsers,Locality_idLocality")] Customer customer)
        {
            customer.AspNetUsers_idAspNetUsers = db.AspNetUsers.Where(user => user.UserName == User.Identity.Name).First().Id;
            
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                Service newOrder = (Service)Session["service"];

                Session["service"] = null;
                Session.Remove("service");

                newOrder.Customer_idCustomer = db.Entry(customer).Entity.idCustomer;

                db.Services.Add(newOrder);
                db.SaveChanges();

                return RedirectToAction("ServiceOrders", "Account");
            }

            ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);
            return View(customer);
        }
        // GET: Service/CheckStatus
        public ActionResult CheckStatus()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("ServiceOrders", "Account");
            else
                return RedirectToAction("Login", "Account");
        }
        // GET: Service/UserPanel
        public ActionResult UserPanel()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Account");
            else
                return RedirectToAction("Login", "Account");
        }
        // GET: Service/Prices
        public ActionResult Prices()
        {
            List<ServiceType> types = db.ServiceTypes.ToList();

            if(types != null)
            {
                return View(types);
            }

            return View();
        }

        public ActionResult OrderDetails(int? id)
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
    }
}