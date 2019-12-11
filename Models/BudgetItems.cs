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
        public enum BudgetItemCatagory { Housing, Transportation, Food, Utilities, Clothing, Medical, Insurance, Personal, Debt, Education, Savings, Entertainment }
        public string Amount { get; set; }
        public string Frequency { get; set; }
        public bool IncomeExpense { get; set; }
        

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