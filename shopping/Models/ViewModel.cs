using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shopping.Models;


namespace shopping.Models
{
    public class ViewModel
    {
        public List<Kategoriler> kategorilers { get; set; }
        public List<Urun> uruns { get; set; }
        public List<Admin> admins { get; set; }
        public List<siteAyar> siteAyars { get; set; }
        public List<Slider> sliders { get; set; }

    }
}