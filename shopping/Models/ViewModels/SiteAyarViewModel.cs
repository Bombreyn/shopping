using System;
using shopping.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shopping
{
    public class SiteAyarViewModel
    {
        public siteAyar siteAyar { get; set; }
        public List<Slider> Slider { get; set; }
        public List<Odeme> odemes { get; set; }
    }

}