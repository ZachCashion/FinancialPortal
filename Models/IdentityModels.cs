using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinancialPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public string AvatarPath { get; set; }

        public int? HouseholdId { get; set; }

        //Nav
        public virtual Households Household { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<Budgets> Budgets { get; set; }
        public virtual ICollection<BankAccounts> BankAccounts { get; set; }

        //Constructor
        public ApplicationUser()
        {
            Transactions = new HashSet<Transactions>();
            Notifications = new HashSet<Notifications>();
            Budgets = new HashSet<Budgets>();
            BankAccounts = new HashSet<BankAccounts>();           
            
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<FinancialPortal.Models.Households> Households { get; set; }

        public System.Data.Entity.DbSet<FinancialPortal.Models.BankAccounts> BankAccounts { get; set; }

        public System.Data.Entity.DbSet<FinancialPortal.Models.Budgets> Budgets { get; set; }

        public System.Data.Entity.DbSet<FinancialPortal.Models.BudgetItems> BudgetItems { get; set; }

        public System.Data.Entity.DbSet<FinancialPortal.Models.Transactions> Transactions { get; set; }

        public System.Data.Entity.DbSet<FinancialPortal.Models.Invitations> Invitations { get; set; }
    }
}