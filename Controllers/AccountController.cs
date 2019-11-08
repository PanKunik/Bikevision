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

namespace bikevision.Controllers
{
    public class AccountController : Controller
    {
        bikewayDBEntities db = new bikewayDBEntities();
        // GET: /Account
        [Authorize(Roles = "Administrator, Uzytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
        public ActionResult Index()
        {

            return View();
        }

        // GET: /Account/PersonalData
        [Authorize(Roles = "Administrator, Uzytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
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


        // GET: /Account/Orders
        [Authorize(Roles = "Administrator, Uzytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
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
        [Authorize(Roles = "Administrator, Uzytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
        public ActionResult ServiceOrders()
        {
            List<Service> listOfOrders;

            string idOfUser = db.AspNetUsers.Where(id => id.UserName == User.Identity.Name).First().Id;
            int customerId = db.Customers.Where(user => user.AspNetUsers_idAspNetUsers == idOfUser).First().idCustomer;

            if (customerId != 0)
            {
                listOfOrders = new List<Service>(db.Services.Where(cust => cust.Customer_idCustomer == customerId).ToList());

                if (listOfOrders.Count() > 0)
                {
                    return View(listOfOrders);
                }
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
        public ActionResult EditPersonalData(Customer customer)
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

                    if (Customers.Count() > 0)
                    {
                        customer = Customers.First();
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        customer.AspNetUsers_idAspNetUsers = db.AspNetUsers.Where(user => user.UserName == User.Identity.Name).First().Id;
                        db.Customers.Add(customer);
                        db.SaveChanges();
                    }

                    return RedirectToAction("PersonalData");
                }

                return View(customer);
            }
        }

        [Authorize(Roles = "Administrator, Uzytkownik, Moderator, Pracownik sklepu, Pracownik serwisu")]
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
                    return RedirectToLocal(returnUrl);
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

                    await this.UserManager.AddToRoleAsync(user.Id, "Uzytkownik");
                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
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