using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bikevision.Models;
using System.Threading.Tasks;

namespace bikevision.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }

        public async Task<string> AddUser()
        {
            ApplicationUser user;
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUserStore Store = new ApplicationUserStore(context);
            ApplicationUserManager userManager = new ApplicationUserManager(Store);
            user = new ApplicationUser
            {
                UserName = "NowyUser",
                Email = "NowyUser@test.com"
            };

            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return result.Errors.First();
            }

            return "User Added";
        }
    }
}