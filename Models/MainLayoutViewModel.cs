using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class MainLayoutViewModel
    {
        public List<ItemType> Types;
        public List<Item> CategoriesOfSpareParts;
        public List<Item> CategoriesAccessories;
        public List<Item> CategoriesOfClothing;
        public List<Item> CategoriesOfTools;
    }
}