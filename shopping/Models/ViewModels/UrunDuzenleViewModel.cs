using shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shopping
{
    public class UrunDuzenleViewModel
    {
        public Urun  Urun { get; set; }
        public List<Kategoriler> kategorilers { get; set; }

    }
}