using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Households
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Greating { get; set; }
        public DateTime Created { get; set; }

        //Nav
        public virtual ICollection<Budgets> Budgets { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } 
        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<Invitations> Invitations { get; set; }

        //Constructor
        public Households()
        {
            Budgets = new HashSet<Budgets>();
            ApplicationUsers = new HashSet<ApplicationUser>();
            Notifications = new HashSet<Notifications>();
            Invitations = new HashSet<Invitations>();
        }

    }
}