using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class HighestQuantity
    {
        public int id;
        public int sum;

        public HighestQuantity(int a , int b)
        {
            id = a;
            sum = b;
        }
    }
}