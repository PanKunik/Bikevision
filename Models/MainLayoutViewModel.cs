using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class MainLayoutViewModel
    {
        public List<ItemType> Types;
        public List<CategoryIdWithName> CategoriesOfSpareParts;
        public List<CategoryIdWithName> CategoriesAccessories;
        public List<CategoryIdWithName> CategoriesOfClothing;
        public List<CategoryIdWithName> CategoriesOfTools;

        public List<CategoryIdWithName> BicyclesByUsage;
        public List<CategoryIdWithName> BicyclesByWheels;
        public List<CategoryIdWithName> BicyclesByBrands;

        public List<CategoryIdWithName> Brands;

    }
}