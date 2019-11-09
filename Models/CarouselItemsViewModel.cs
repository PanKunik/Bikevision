using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class CarouselItemsViewModel
    {
            public List<Item> ItemsOnPromotion { get; set; }
            public List<Item> NewestItems { get; set; }
            public List<Item> PopularItems { get; set; }
    }
}