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
            context.Households.AddOrUpdate(
                h => h.Name,
                    new Households { Name = "Cashion", Greating = "Welcome", Created = DateTime.Now } 
                );
            context.SaveChanges();

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
                    AvatarPath = "/Avatar/avatarPlaceholder.png",
                    HouseholdId = 1
                }, demoPassword);
            }

            //Assign Roles
            var adminId = userManager.FindByEmail("DemoAdmin@Mailinator.com").Id;
            userManager.AddToRole(adminId, "HouseholdHead");

            context.SaveChanges();

            var user = userManager.FindByEmail("DemoAdmin@Mailinator.com");


            //Bank Accounts
            context.BankAccounts.AddOrUpdate(
                new BankAccounts { Id = 1, HouseholdId = 1, Created = DateTime.Now, Name = "Checking", OwnerId = user.Id, StartingBalance = "10000", CurrentBalance = "10000" },
                new BankAccounts { Id = 2, HouseholdId = 1, Created = DateTime.Now, Name = "Savings", OwnerId = user.Id, StartingBalance = "10000", CurrentBalance = "10000" }
                );

            //Budgets
            context.Budgets.AddOrUpdate(
                new Budgets { Id = 1, HouseholdId = 1, OwnerId = user.Id, Created = DateTime.Now, Name = "MyBudget", TargetAmount = "10000", CurrentAmount = "10000" }
                );

            context.SaveChanges();

            //Transactions
            context.Transactions.AddOrUpdate(
                new Transactions { BankAccountId = 1, OwnerId = user.Id, Created = DateTime.Now, Amount = "1000", Memo = "new", DipositWithdraw = false  }
                );

            //Budget Items
            context.BudgetItems.AddOrUpdate(
                new BudgetItems { BudgetId = 1, Created = DateTime.Now, Name = "Food", Amount = "100", Frequency = "2", IncomeExpense = false}
                );
        }
    }
}
