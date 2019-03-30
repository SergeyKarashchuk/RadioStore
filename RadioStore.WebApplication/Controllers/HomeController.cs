using Microsoft.AspNet.Identity.Owin;

using RadioStore.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RadioStore.WebApplication.Controllers
{    
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        
        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            IList<string> roles = new List<string> { "No roles" };
            var userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                roles = await userManager.GetRolesAsync(user.Id);
            }

            return View(roles);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}