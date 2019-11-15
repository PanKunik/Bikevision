using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class AccountDataViewModel
    {
        public List<Sale> Sales;
        public List<Service> Services;
        public int discount;
    }
}