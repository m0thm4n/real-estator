using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RealEstator.Models;

[assembly: OwinStartupAttribute(typeof(RealEstator.Startup))]
namespace RealEstator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        // Creating default users roles and Admin user for login
        private void createRoleandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Create admin role first and a default admin user
            if (!roleManager.RoleExists("Admin"))
            {
                // first create Admin role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                // Create a Admin super user who will maintain the website

                var user = new ApplicationUser();
                user.UserName = "mothman";
                user.Email = "nathan.moritz@protonmail.com";

                string userPass = "UH@v3B33nH@ck3d!";

                var checkUser = userManager.Create(user, userPass); ;

                // Add default User to Role Admin
                if (checkUser.Succeeded)
                {
                    var resultOne = userManager.AddToRole(user.Id, "Admin");
                }
            }

            // Creating Manager role
            if (!roleManager.RoleExists("Renter"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Renter";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Tenant"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Tenant";
                roleManager.Create(role);
            }
        }
    }
}
