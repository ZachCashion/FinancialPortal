using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Budgets
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string OwnerId { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string TargetAmount { get; set; }
        public string CurrentAmount { get; set; }

        //Nav
        public virtual Households Household { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<BudgetItems> BudgetItems { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }

        //Constructor
        public Budgets()
        {
            BudgetItems = new HashSet<BudgetItems>();
            Transactions = new HashSet<Transactions>();
        }
    }
}