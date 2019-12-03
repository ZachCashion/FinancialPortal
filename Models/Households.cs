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
    }
}