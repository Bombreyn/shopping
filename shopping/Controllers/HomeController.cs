using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using shopping.Models;
using System.Web.Security; //FormsAuthentication kullanmak için
using System.Data.Entity;
using shopping.App_Start;
using System.Net;
using System.IO;

namespace shopping.Controllers
{
    public class HomeController : Controller
    {
        ShoppingContext db = new ShoppingContext();

        public ActionResult Index()
        {
            try
            {
                SiteAyarViewModel model = new SiteAyarViewModel();
                model.Slider = db.Slider.ToList();


                    return View(model);

            }
            catch (Exception)
            {
                ViewBag.Message = "Bilinmeyen bir hata meydana geldi";
                return View();
            }
        }

        public ActionResult Urun(int id, string urunadi, string kategoriadi)
        {
            try
            {
                var urun = db.Urun.Where(x => x.urunID == id).FirstOrDefault();
                if (seo.Seo.AdresDuzenle(urun.isim) == urunadi && seo.Seo.AdresDuzenle(urun.Kategoriler.adi) == kategoriadi)
                {
                    return View(urun);
                }
                else
                {
                    ViewBag.Message = "Bilinmeyen bir hata meydana geldi";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {
                ViewBag.Message = "Bilinmeyen bir hata meydana geldi";
                return View();
            }
        } 
        
        public ActionResult Urunler()
        {
            try
            {
                ViewModel vw = new ViewModel()
                {
                    uruns = db.Urun.ToList(),
                    kategorilers = db.Kategoriler.ToList()
                };
                return View(vw);

            }
            catch (Exception)
            {
                ViewBag.Message = "Bilinmeyen bir hata meydana geldi";
                return View();
            }
        }

        public ActionResult iletisim()
        {
            try
            {
                SiteAyarViewModel model = new SiteAyarViewModel();
                model.odemes = db.Odeme.ToList();
                return View(model);

            }
            catch (Exception)
            {
                ViewBag.Message = "Bilinmeyen bir hata meydana geldi";
                return View();
            }
        }

        public ActionResult Hata()
        {
            try
            {
                siteAyar siteAyar = new siteAyar();

                return View(siteAyar);

            }
            catch (Exception)
            {
                ViewBag.Message = "Bilinmeyen bir hata meydana geldi";
                return View();
            }
        }
}
}