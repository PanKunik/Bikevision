using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class ComparationViewModel
    {

        public Item ComparationItem { get; set; }
        public List<Tuple<string,List<string>>> FeaturesList { get; set; }
    }
}