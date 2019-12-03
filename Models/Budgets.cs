using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Budgets
    {
        public int Id { get; set; }
        public string HouseholdId { get; set; }
        public string OwnerId { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string TargetAmount { get; set; }
        public string CurrentAmount { get; set; }
    }
}