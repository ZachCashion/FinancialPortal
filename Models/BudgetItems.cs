using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class BudgetItems
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string TargetAmount { get; set; }
        public string CurrentAmount { get; set; }

        //Nav
        public virtual Budgets Budget { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }

        //Constructor
        public BudgetItems()
        {
            Transactions = new HashSet<Transactions>();
        }

    }
}