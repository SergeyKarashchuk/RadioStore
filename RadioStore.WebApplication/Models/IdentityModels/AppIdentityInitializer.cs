using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RadioStore.WebApplication.Models.IdentityModels
{
    public class AppIdentityInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //create roles
            var adminRole = new IdentityRole("admin");
            var userRole = new IdentityRole("user");

            //add rolses to db
            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            //create first admin
            var admin = new ApplicationUser
            {
                Email = "sportstoreadmin@ukr.net",
                UserName = "sportstoreadmin@ukr.net"
            };
            var adminPassword = "ssadmin123";
            var resAdmin = userManager.Create(admin, adminPassword);


            if (resAdmin.Succeeded)
            {
                //add roles to masteradmin
                userManager.AddToRole(admin.Id, adminRole.Name);
                userManager.AddToRole(admin.Id, userRole.Name);
            }

            //create first test user
            var user = new ApplicationUser
            {
                Email = "sstestuser@ukr.net",
                UserName = "sstestuser@ukr.net"
            };

            var userPassword = "ssuser123";
            var resUser = userManager.Create(user, userPassword);

            if (resUser.Succeeded)
            {
                //add role to test user
                userManager.AddToRole(user.Id, userRole.Name);
            }



            base.Seed(context);
        }
    }
}