using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public int BudgetItemId { get; set; }
        public int OwnerId { get; set; }
        public enum TransactionType {  }
        public DateTime Created { get; set; }
        public string Amount { get; set; }
        public string Memo { get; set; }


        //Nav 
        public virtual BankAccounts BankAccount { get; set; }
        public virtual BudgetItems BudgetItem { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}