using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class ProductDetailsViewModel
    {
        public Item product { get; set; }
        public List<Tuple<string, List<string>, bool>> FeaturesList { get; set; }
        public List<Opinion> opinions;
        public bool isEligibleToAddOpinion;
        public int HiddenQuantity;
    }
}