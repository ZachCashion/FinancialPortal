﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Notifications
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string RecipientId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool IsRead { get; set; }

        //Nav
        public virtual Households Household { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
    }
}