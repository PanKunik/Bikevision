using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class CategoryIdWithName
    {
        public int id;
        public string name;

        public CategoryIdWithName(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}