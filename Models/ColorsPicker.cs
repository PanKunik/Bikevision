using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace bikevision.Models
{
    public static class ColorsPicker
    {
        public static NameValueCollection ListOfColors = new NameValueCollection()
        {
            { "czerwony", "#FF0000" },
            { "zielony", "#00FF00" },
            { "niebieski", "#0000FF" }
        };
    }
}