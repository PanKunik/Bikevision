using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class Order
    {
        public Sale sale { get; set; }
        public Customer customer { get; set; }

    }
}