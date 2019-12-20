using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace FinancialPortal.Helpers
{
    public class DashboardHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        

        public List<Transactions> ListTransactions()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var houseId = user.HouseholdId;
            var house = db.Households.Find(houseId);

            var transactions = new List<Transactions>();
            transactions.AddRange(house.ApplicationUsers.SelectMany(t => t.Transactions));

            return transactions;
        }

        public List<BankAccounts> ListBankAccounts()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var houseId = user.HouseholdId;
            var house = db.Households.Find(houseId);

            var bankAccounts = new List<BankAccounts>();
            bankAccounts.AddRange(house.ApplicationUsers.SelectMany(a => a.BankAccounts));

            return bankAccounts;

        }

        public List<BudgetItems> ListBudgetItems()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var houseId = user.HouseholdId;
            var house = db.Households.Find(houseId);

            var budgetItems = new List<BudgetItems>();
            budgetItems.AddRange(house.Budgets.SelectMany(b => b.BudgetItems));

            return budgetItems;

        }
    }
}