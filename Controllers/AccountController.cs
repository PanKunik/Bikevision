using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using bikevision.Controllers;
using bikevision.Models;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Net;

namespace bikevision.Controllers
{
    public class AccountController : Controller
    {
        public MainLayoutViewModel MainLayoutViewModel { get; set; }

        public AccountController()
        {
            this.MainLayoutViewModel = new MainLayoutViewModel();
            this.MainLayoutViewModel.Types = db.ItemTypes.ToList();
            this.MainLayoutViewModel.CategoriesOfSpareParts = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesAccessories = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesOfTools = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesOfClothing = new List<CategoryIdWithName>();

            this.MainLayoutViewModel.BicyclesByUsage = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.BicyclesByBrands = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.BicyclesByWheels = new List<CategoryIdWithName>();

            this.MainLayoutViewModel.Brands = new List<CategoryIdWithName>();
            // this.MainLayoutViewModel.CategoriesOfSpareParts 
            IQueryable<Item> allItems = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Include(b => b.Brand);


            List<Item> itemsSpareParts = allItems.Where(type => type.ItemType.type == "Części zamienne").ToList();
            //  db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Części zamienne").ToList();
            List<Item> itemsAccessories = allItems.Where(type => type.ItemType.type == "Akcesoria").ToList();
            //  db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Akcesoria").ToList();
            List<Item> itemsClothing = allItems.Where(type => type.ItemType.type == "Odzież").ToList();
            //  db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Odzież").ToList();
            List<Item> itemsTools = allItems.Where(type => type.ItemType.type == "Narzędzia").ToList();
            //  db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Narzędzia").ToList();

            List<Item> bicyclesUsages = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Rowery").ToList();
            List<Item> bicyclesBrands = db.Items.Include(brand => brand.Brand).Include(type => type.ItemType).Where(type => type.ItemType.type == "Rowery").ToList();
            List<FeatureValueOfItem> bicyclesWheels = db.FeatureValueOfItems.Include(feat => feat.Feature).Where(type => type.Item.ItemType.type == "Rowery").Where(feature => feature.Feature.feature1 == "Rozmiar kół").ToList();

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

            foreach (var bicycle in bicyclesUsages)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(bicycle.Category_idCategory, bicycle.Category.category1);

                if (this.MainLayoutViewModel.BicyclesByUsage.Where(id => id.id == bicycle.Category_idCategory).Where(name => name.name == bicycle.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.BicyclesByUsage.Add(newCat);
            }

            if (this.MainLayoutViewModel.BicyclesByUsage.Count() > 0)
                this.MainLayoutViewModel.BicyclesByUsage = this.MainLayoutViewModel.BicyclesByUsage.OrderBy(name => name.name).ToList();


            //Make bicycles by wheels
            foreach (var bicycle in bicyclesWheels)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(bicycle.FeatureValue_idFeatureValue, bicycle.FeatureValue.featureValue1);

                if (this.MainLayoutViewModel.BicyclesByWheels.Where(id => id.id == bicycle.FeatureValue_idFeatureValue).Where(name => name.name == bicycle.FeatureValue.featureValue1).Count() <= 0)
                    this.MainLayoutViewModel.BicyclesByWheels.Add(newCat);
            }

            if (this.MainLayoutViewModel.BicyclesByWheels.Count() > 0)
                this.MainLayoutViewModel.BicyclesByWheels = this.MainLayoutViewModel.BicyclesByWheels.OrderBy(name => name.name).ToList();


            //Make bicycles by usage
            foreach (var bicycle in bicyclesBrands)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(bicycle.Brand_idBrand, bicycle.Brand.brand1);

                if (this.MainLayoutViewModel.BicyclesByBrands.Where(id => id.id == bicycle.Brand_idBrand).Where(name => name.name == bicycle.Brand.brand1).Count() <= 0)
                    this.MainLayoutViewModel.BicyclesByBrands.Add(newCat);
            }

            if (this.MainLayoutViewModel.BicyclesByBrands.Count() > 0)
                this.MainLayoutViewModel.BicyclesByBrands = this.MainLayoutViewModel.BicyclesByBrands.OrderBy(name => name.name).ToList();

            foreach (var item in allItems)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Brand_idBrand, item.Brand.brand1);

                if (this.MainLayoutViewModel.Brands.Where(id => id.id == item.Brand_idBrand).Where(name => name.name == item.Brand.brand1).Count() <= 0)
                    this.MainLayoutViewModel.Brands.Add(newCat);
            }

            if (this.MainLayoutViewModel.Brands.Count() > 0)
                this.MainLayoutViewModel.Brands = this.MainLayoutViewModel.Brands.OrderBy(name => name.name).ToList();

            this.ViewData["MainLayoutViewModel"] = this.MainLayoutViewModel;
        }

        bikewayDBEntities db = new bikewayDBEntities();
        // GET: /Account
        [Authorize(Roles = "Administrator, Użytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
        public ActionResult Index()
        {
            AccountDataViewModel accountData = new AccountDataViewModel();
            accountData.Sales = new List<Sale>();
            accountData.Services = new List<Service>();
            accountData.discount = 0;

            List<Sale> customerSales = db.Sales.Where(sales => sales.Customer.AspNetUser.UserName == User.Identity.Name).OrderByDescending(date => date.date).Take(3).ToList();

            if (customerSales.Count() > 0)
                accountData.Sales = customerSales;

            List<Service> customerServices = db.Services.Where(sales => sales.Customer.AspNetUser.UserName == User.Identity.Name).OrderByDescending(date => date.dateOfEmployment).Take(3).ToList();

            if (customerServices.Count() > 0)
                accountData.Services = customerServices;

            List<Customer> custs = db.Customers.Where(cust => cust.AspNetUser.UserName == User.Identity.Name).ToList();

            if (custs.Count() > 0)
                accountData.discount = (custs.First().PermanentDiscount_idPermanentDiscount != null) ? custs.First().PermanentDiscount.discount : 0;

            return View(accountData);
        }

        // GET: /Account/PersonalData
        [Authorize(Roles = "Administrator, Użytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
        public ActionResult PersonalData()
        {
            if(User.Identity.IsAuthenticated)
            {
                Customer customerData = new Customer();

                string idOfUser = db.AspNetUsers.Where(id => id.UserName == User.Identity.Name).First().Id;
                IQueryable<Customer> customers = db.Customers.Where(user => user.AspNetUsers_idAspNetUsers == idOfUser);

                if (customers.Count() > 0)
                {
                    int customerId = customers.First().idCustomer;

                    if (customerId != 0)
                    {
                        customerData = db.Customers.Where(user => user.idCustomer == customerId).First();
                        return View(customerData);
                    }
                    return View();
                }
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult AddPersonalData()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1");
                return View();
            }
            return RedirectToAction("Index", "Account");
        }        

        // GET: /Account/Orders
        [Authorize(Roles = "Administrator, Użytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
        public ActionResult Orders()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Sale> listOfOrders;

                string idOfUser = db.AspNetUsers.Where(id => id.UserName == User.Identity.Name).First().Id;
                IQueryable<Customer> customers = db.Customers.Where(user => user.AspNetUsers_idAspNetUsers == idOfUser);

                if (customers.Count() > 0)
                {
                    int customerId = customers.First().idCustomer;

                    if (customerId != 0)
                    {
                        listOfOrders = new List<Sale>(db.Sales.Where(cust => cust.Customer_idCustomer == customerId).ToList());

                        if (listOfOrders.Count() > 0)
                        {
                            decimal[] sum = new decimal[listOfOrders.Count];

                            int index = 0;
                            foreach (var sale in listOfOrders)
                            {
                                foreach (var value in sale.SaleDetails)
                                {
                                    sum[index] += value.value;
                                }

                                index++;
                            }

                            ViewBag.OrdersSum = sum;

                            return View(listOfOrders);
                        }
                        return View();
                    }
                    return View();
                }
                return View();
            }
            return View();
        }
        // GET: /Account/Orders
        [Authorize(Roles = "Administrator, Użytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
        public ActionResult ServiceOrders()
        {
            List<Service> listOfOrders;

            string idOfUser = db.AspNetUsers.Where(id => id.UserName == User.Identity.Name).First().Id;
            IQueryable<Customer> customers = db.Customers.Where(user => user.AspNetUsers_idAspNetUsers == idOfUser);

            if (customers.Count() > 0)
            {
                int customerId = customers.First().idCustomer;

                if (customerId != 0)
                {
                    listOfOrders = new List<Service>(db.Services.Where(cust => cust.Customer_idCustomer == customerId).ToList());

                    if (listOfOrders.Count() > 0)
                    {
                        return View(listOfOrders);
                    }
                    return View();
                }
                return View();
            }
            return View();
        }

        public ActionResult EditPersonalData()
        {
            ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1");

            if (User.Identity.IsAuthenticated)
            {
                IQueryable<Customer> existingCustomer = db.Customers.Where(cust => cust.AspNetUser.UserName == User.Identity.Name);

                if (existingCustomer.Count() > 0)
                    return View(existingCustomer.First());
                else
                    return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPersonalData([Bind(Include = "idCustomer,name,surname,telephoneNumber,emailAddress,addressOfResidence,zipCode,AspNetUsers_idAspNetUsers,Locality_idLocality")]Customer customer)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);

                if (ModelState.IsValid)
                {
                    IQueryable<Customer> Customers = db.Customers.Where(cust => cust.AspNetUser.UserName == User.Identity.Name);
                    string locality = Request["Locality.locality1"];

                    List<Locality> l = db.Localities.Where(loc => loc.locality1.ToLower().Equals(locality.ToLower())).ToList();
                    string local = "";

                    if (l.Count() > 0)
                    {
                        local = l.First().locality1;
                    }
                    else
                    {
                        Locality newLocality = new Locality();
                        newLocality.locality1 = locality;
                        db.Localities.Add(newLocality);
                        db.SaveChanges();
                        l = db.Localities.Where(loc => loc.locality1.ToLower().Equals(locality.ToLower())).ToList();
                    }

                    if (Customers.Count() > 0)
                    {
                        //customer.idCustomer = existingCust.First().idCustomer;
                        //customer.AspNetUsers_idAspNetUsers = db.AspNetUsers.Where(user => user.UserName == User.Identity.Name).First().Id;
                        customer.AspNetUsers_idAspNetUsers = db.AspNetUsers.Where(user => user.UserName == User.Identity.Name).First().Id;
                        customer.Locality_idLocality = l.First().idLocality;

                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        customer.AspNetUsers_idAspNetUsers = db.AspNetUsers.Where(user => user.UserName == User.Identity.Name).First().Id;
                        customer.Locality_idLocality = l.First().idLocality;

                        db.Customers.Add(customer);
                        db.SaveChanges();
                    }

                    return RedirectToAction("PersonalData");
                }

                return View(customer);
            }
        }

        [Authorize(Roles = "Administrator, Użytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
        public ActionResult ManagementPanel()
        {
            return View();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        string thisUserId = (db.AspNetUsers.Where(name => name.UserName == model.Email).ToList().Count() > 0) ? db.AspNetUsers.Where(name => name.UserName == model.Email).ToList().First().Id : "";

                        if (thisUserId == "" || thisUserId == null)
                            return RedirectToLocal(returnUrl);

                        List<AspNetUserFavorite> favoriteItems = db.AspNetUserFavorites.Where(items => items.AspNetUsers_Id == thisUserId).ToList();

                        if(favoriteItems.Count() <= 0)
                            return RedirectToLocal(returnUrl);

                        List<Item> favItems = new List<Item>();

                        foreach(var it in favoriteItems)
                        {
                            favItems.Add(db.Items.Where(i => i.idItem == it.Item_idItem).ToList().First());
                        }

                        Session["favoriteItems"] = favItems;

                        return RedirectToLocal(returnUrl);
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Nie znaleziono użytkownika z takim adresem email i hasłem.");
                    return View(model);
            }
        }
        
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    await this.UserManager.AddToRoleAsync(user.Id, "Użytkownik");
                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
            List<SaleDetail> saleDetail = db.SaleDetails.Where(i => i.Sale_idSale == id).ToList();
            if (saleDetail == null || saleDetail.Count() <= 0)
            {
                return HttpNotFound();
            }
            if(saleDetail.First().Sale.Customer_idCustomer != idOfCustomer)
            {
                return HttpNotFound();
            }

            return View(saleDetail);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}