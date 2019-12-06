﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class BankAccounts
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string OwnerId { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public enum AccountType { Checking, Savings, Credit }
        public string StartingBalance { get; set; }
        public string CurrentBalance { get; set; }

        //Nav
        public virtual Households Household { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }

        //Constructor
        public BankAccounts()
        {
            Transactions = new HashSet<Transactions>();
        }
    }
}