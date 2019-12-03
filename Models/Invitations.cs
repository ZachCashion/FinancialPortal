using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Invitations
    {
        public int Id { get; set; }
        public string HouseholdId { get; set; }
        public bool IsValid { get; set; }
        public DateTime Created { get; set; }
        public string TTL { get; set; }
        public string RecipientEmail { get; set; }
        public string Code { get; set; }
    }
}