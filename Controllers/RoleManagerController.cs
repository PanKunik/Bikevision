using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using bikevision.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace bikevision.Controllers
{
    public class RoleManagerController : Controller
    {
        private bikewayDBEntities db = new bikewayDBEntities();
        // GET: RoleManager
        [Authorize(Roles ="Administrator")]
        public async Task<ActionResult> Index()
        {
            List<AspNetUser> users = db.AspNetUsers.ToList();

            List<Tuple<string, string>> userIdWithRole = new List<Tuple<string, string>>();
            foreach (var u in users)
            {
                var result = await UserManager.GetRolesAsync(u.Id);
                foreach (var r in result)
                {
                    if(!r.Contains("Admin"))
                        userIdWithRole.Add(new Tuple<string, string>(u.UserName, r));
                }
            }

            Session["roles"] = db.AspNetRoles.Where(role => role.Name != "Administrator").ToList();
            
            return View(userIdWithRole);
        }

        private ApplicationUserManager _userManager;
        
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

        [Authorize(Roles = "Administrator")]
        // GET: RoleManager/Details/5
        public ActionResult EditRole(FormCollection collection, string userId)
        {
            string newRole = collection["role"];

            string user = db.AspNetUsers.Where(i => i.UserName == userId).First().Id;
            string role = db.AspNetRoles.Where(i => i.Id == newRole).First().Name;

            string[] allRoles = db.AspNetRoles.Select(r => r.Name).ToArray();

            foreach(var s in allRoles)
            UserManager.RemoveFromRole(user, s);

            UserManager.AddToRole(user, role);

            return RedirectToAction("Index");
        }
    }
}
