using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public string BankAccountId { get; set; }
        public string BudgetItemId { get; set; }
        public string OwnerId { get; set; }
        public string TransactionTypeId { get; set; }
        public DateTime Created { get; set; }
        public string Amount { get; set; }
        public string Memo { get; set; }

    }
}