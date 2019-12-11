namespace FinancialPortal.Migrations
{
    using FinancialPortal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<FinancialPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FinancialPortal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "HouseholdHead"))
            {
                roleManager.Create(new IdentityRole { Name = "HouseholdHead" });
            }

            //Households
            
            

            //User Creation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];

            if (!context.Users.Any(u => u.Email == "DemoAdmin@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@Mailinator.com",
                    Email = "DemoAdmin@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "DemoAdmin",
                    AvatarPath = "/Avatar/avatarPlaceholder.png"
                    
                }, demoPassword);
            }

            //Assign Roles
            var adminId = userManager.FindByEmail("DemoAdmin@Mailinator.com").Id;
            userManager.AddToRole(adminId, "HouseholdHead");


            //Bank Accounts

            //Transactions

            //Budgets

            //Budget Items
            
        }
    }
}
