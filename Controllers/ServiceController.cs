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
        public MainLayoutViewModel MainLayoutViewModel { get; set; }

        public ServiceController()
        {
            this.MainLayoutViewModel = new MainLayoutViewModel();
            this.MainLayoutViewModel.Types = db.ItemTypes.ToList();
            this.MainLayoutViewModel.CategoriesOfSpareParts = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesAccessories = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesOfTools = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesOfClothing = new List<CategoryIdWithName>();
            // this.MainLayoutViewModel.CategoriesOfSpareParts 

            List<Item> itemsSpareParts = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Części zamienne").ToList();
            List<Item> itemsAccessories = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Akcesoria").ToList();
            List<Item> itemsClothing = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Odzież").ToList();
            List<Item> itemsTools = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Narzędzia").ToList();

            foreach (var item in itemsSpareParts)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if (this.MainLayoutViewModel.CategoriesOfSpareParts.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfSpareParts.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfSpareParts.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfSpareParts = this.MainLayoutViewModel.CategoriesOfSpareParts.OrderBy(name => name.name).ToList();

            foreach (var item in itemsAccessories)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if (this.MainLayoutViewModel.CategoriesAccessories.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesAccessories.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesAccessories.Count() > 0)
                this.MainLayoutViewModel.CategoriesAccessories = this.MainLayoutViewModel.CategoriesAccessories.OrderBy(name => name.name).ToList();

            foreach (var item in itemsClothing)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if (this.MainLayoutViewModel.CategoriesOfClothing.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfClothing.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfClothing.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfClothing = this.MainLayoutViewModel.CategoriesOfClothing.OrderBy(name => name.name).ToList();

            foreach (var item in itemsTools)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if (this.MainLayoutViewModel.CategoriesOfTools.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfTools.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfTools.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfTools = this.MainLayoutViewModel.CategoriesOfTools.OrderBy(name => name.name).ToList();

            this.ViewData["MainLayoutViewModel"] = this.MainLayoutViewModel;
        }

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

                ServiceState stateId = db.ServiceStates.Where(i => i.state == "oczekuje na diagnozę").First();
                service.ServiceState_idServiceState = stateId.idServiceState;

                service.dateOfEmployment = DateTime.Now;

                service.price = 0;

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

                if (Session["service"] != null)
                {
                    Service newOrder = (Service)Session["service"];
                    Session["service"] = null;
                    Session.Remove("service");

                    newOrder.Customer_idCustomer = db.Entry(customer).Entity.idCustomer;

                    db.Services.Add(newOrder);
                    db.SaveChanges();

                    return RedirectToAction("ServiceOrders", "Account");
                }
                else
                {
                    return RedirectToAction("PersonalData", "Account");
                }
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

        public ActionResult NoteToOrder(int? id)
        {
            Note newNote = new Note();
            NoteToService noteToService = new NoteToService();
            if (Request["note"] != null)
            {
                newNote.note1 = Request["note"];
                db.Notes.Add(newNote);
                db.SaveChanges();

                noteToService.date = DateTime.Now;
                noteToService.Note_idNote = db.Entry(newNote).Entity.idNote;
                noteToService.Service_idService = (int)id;
                noteToService.AspNetUser_idAspNetUser = db.AspNetUsers.Where(userName => userName.UserName == User.Identity.Name).First().Id;

                db.NoteToServices.Add(noteToService);
                db.SaveChanges();
            }

            return RedirectToAction("OrderDetails", new { id = id });
        }

        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Customer> customer = db.Customers.Where(i => i.AspNetUser.UserName == User.Identity.Name).ToList();
            if (customer == null || customer.Count() <= 0)
            {
                return HttpNotFound();
            }

            int idOfCustomer = customer.First().idCustomer;
            Service service = db.Services.Find(id);

            ServiceWithNotesViewModel orderWithNotes = new ServiceWithNotesViewModel();

            if (service == null || service.Customer_idCustomer != idOfCustomer)
            {              
                return HttpNotFound();
            }

            orderWithNotes.serviceOrder = service;
            List<NoteToService> listOfNotes = db.NoteToServices.Where(note => note.Service_idService == service.idService).OrderByDescending(i => i.Note_idNote).ToList();

            if (listOfNotes.Count() > 0)
            {
                orderWithNotes.notesToOrder = listOfNotes;
            }

            return View(orderWithNotes);
        }
    }
}