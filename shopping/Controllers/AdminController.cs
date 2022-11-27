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
    public class AdminController : Controller
    {
        ShoppingContext db = new ShoppingContext();

        // GET: Admin
        public ActionResult Login()
        {
            var siteisim = db.siteAyar.FirstOrDefault();
            Session["siteisim"] = siteisim.site_adi;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // kullanıcılardan veya yöneticilerden gelen isteklere (request) isteklerin doğruluğunu Tokenler aracılığıyla anlayıp ona göre cevap verir
        public ActionResult AdminLogin(Admin form)
        {
            ViewModel vw = new ViewModel();

            if (!ModelState.IsValid)  // box lar doğru validation şekilde doldurulmasını kontrol ediyor.
            {

                return View("Login", form);
            }


            using (ShoppingContext db = new ShoppingContext())
            {

                var AdminVarmi = db.Admin.FirstOrDefault(
                    x => x.adi_soyadi == form.adi_soyadi && x.sifre == form.sifre); //kullanıcı olup olmadığını kontrol ediyor.

                var siteisim = db.siteAyar.FirstOrDefault();
                if (AdminVarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(AdminVarmi.adi_soyadi, false); //kullanıcı kayıtlı kalıncaksa true olacak. (cookie)
                    Session["siteisim"] = siteisim.site_adi;
                    Proje.Aktif.adminData = AdminVarmi;
                    return RedirectToAction("Urunler", "Admin");
                }


                ViewBag.Hata = "kullanıcı adı veya şifre hatalı"; //hata mesajı
                return View("Login"); // kullanıcı yok ise geri login sayfasına yönlendiriyor.

            }
        } //login post işlemleri ve kontrol..

        public ActionResult Logout()
        {
            Session.Abandon(); // seansı yok eder.
            FormsAuthentication.SignOut(); //cookie yi yok eder.
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Urunler()
        {

            try
            {
                ViewModel vw = new ViewModel()
                {

                    uruns = db.Urun.ToList(),
                    kategorilers = db.Kategoriler.ToList(),
                    admins = db.Admin.ToList()

                };
                return View(vw);

            }
            catch (Exception)
            {
                ViewBag.Message = "Bilinmeyen bir hata meydana geldi";
                return View();
            }



        }

        [Authorize]
        public ActionResult UrunDuzenle(int id, string urunadi)
        {
            try
            {
                UrunDuzenleViewModel model = new UrunDuzenleViewModel();
                model.kategorilers = db.Kategoriler.ToList();
                model.Urun = db.Urun.Where(x => x.urunID == id).FirstOrDefault();

                if (seo.Seo.AdresDuzenle(model.Urun.isim) == urunadi)
                {

                    return View(model);
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

        [HttpPost]
        [Authorize]
        public ActionResult Duzenle(int? urunID, string isim, string aciklama, string urunkodu, string fiyat, string link, string kod, int? kategori, Urun kapakresim, Urun aciklamaresim, Urun resim1, Urun resim2, Urun resim3, Urun resim4, Urun resim5, Urun resim6, bool? aktif, bool? stok)
        {

            Urun urun = db.Urun.Where(x => x.urunID == urunID).FirstOrDefault();

            urun.isim = isim;
            urun.aciklama = aciklama;
            urun.urunkodu = urunkodu;
            urun.fiyat = fiyat;
            urun.satin_al = link;
            urun.kod = kod;
            urun.stok = stok;
            urun.kategoriID = kategori;
            urun.tarih = DateTime.Now;
            urun.aktif = aktif;
            urun.adminID = Proje.Aktif.adminData.adminID;

            if (Request.Files.Count > 0)
            {
                string dosyaadikapakresim = Path.GetFileName(Request.Files[0].FileName);
                string dosyaadiaciklamaresim = Path.GetFileName(Request.Files[1].FileName);
                string dosyaadiresim1 = Path.GetFileName(Request.Files[2].FileName);
                string dosyaadiresim2 = Path.GetFileName(Request.Files[3].FileName);
                string dosyaadiresim3 = Path.GetFileName(Request.Files[4].FileName);
                string dosyaadiresim4 = Path.GetFileName(Request.Files[5].FileName);
                string dosyaadiresim5 = Path.GetFileName(Request.Files[6].FileName);
                string dosyaadiresim6 = Path.GetFileName(Request.Files[7].FileName);


                if(dosyaadikapakresim != null && dosyaadikapakresim !="")
                {
                    string _filenamekapakresim = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadikapakresim;
                    string yolkapakresim = "~/Content/images/resim/urun/kapakresim/" + _filenamekapakresim;
                    if (yolkapakresim != "~/Content/images/resim/urun/kapakresim/" && _filenamekapakresim !=null && _filenamekapakresim !="")
                    {
                        Request.Files[0].SaveAs(Server.MapPath(yolkapakresim));
                        urun.kapakresim = "/Content/images/resim/urun/kapakresim/" + _filenamekapakresim;
                    }
                }
                if (dosyaadiaciklamaresim != null && dosyaadiaciklamaresim != "")
                {
                    string _filenameaciklamaresim = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadiaciklamaresim;
                    string yolaciklamaresim = "~/Content/images/resim/urun/aciklamaresim/" + _filenameaciklamaresim;
                    if (yolaciklamaresim != "~/Content/images/resim/urun/aciklamaresim/" && _filenameaciklamaresim != null && _filenameaciklamaresim != "")
                    {
                        Request.Files[1].SaveAs(Server.MapPath(yolaciklamaresim));
                        urun.aciklamaresim = "/Content/images/resim/urun/aciklamaresim/" + _filenameaciklamaresim;
                    }
                }
                if (dosyaadiresim1 != null && dosyaadiresim1 != "")
                {
                    string _filenameresim1 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadiresim1;
                    string yolresim1 = "~/Content/images/resim/urun/resim1/" + _filenameresim1;
                    if (yolresim1 != "~/Content/images/resim/urun/resim1/" && _filenameresim1 != null && _filenameresim1 != "")
                    {
                        Request.Files[2].SaveAs(Server.MapPath(yolresim1));
                        urun.resim1 = "/Content/images/resim/urun/resim1/" + _filenameresim1;
                    }
                }
                if (dosyaadiresim2 != null && dosyaadiresim2 != "")
                {
                    string _filenameresim2 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadiresim2;
                    string yolresim2 = "~/Content/images/resim/urun/resim2/" + _filenameresim2;
                    if (yolresim2 != "~/Content/images/resim/urun/resim2/" && _filenameresim2 != null && _filenameresim2 != "")
                    {
                        Request.Files[3].SaveAs(Server.MapPath(yolresim2));
                        urun.resim2 = "/Content/images/resim/urun/resim2/" + _filenameresim2;
                    }
                }
                if (dosyaadiresim3 != null && dosyaadiresim3 != "")
                {
                    string _filenameresim3 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadiresim3;
                    string yolresim3 = "~/Content/images/resim/urun/resim3/" + _filenameresim3;
                    if (yolresim3 != "~/Content/images/resim/urun/resim3/" && _filenameresim3 != null && _filenameresim3 != "")
                    {
                        Request.Files[4].SaveAs(Server.MapPath(yolresim3));
                        urun.resim3 = "/Content/images/resim/urun/resim3/" + _filenameresim3;
                    }
                }
                if (dosyaadiresim4 != null && dosyaadiresim4 != "")
                {
                    string _filenameresim4 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadiresim4;
                    string yolresim4 = "~/Content/images/resim/urun/resim4/" + _filenameresim4;
                    if (yolresim4 != "~/Content/images/resim/urun/resim4/" && _filenameresim4 != null && _filenameresim4 != "")
                    {
                        Request.Files[5].SaveAs(Server.MapPath(yolresim4));
                        urun.resim4 = "/Content/images/resim/urun/resim4/" + _filenameresim4;
                    }
                }
                if (dosyaadiresim5 != null && dosyaadiresim5 != "")
                {
                    string _filenameresim5 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadiresim5;
                    string yolresim5 = "~/Content/images/resim/urun/resim5/" + _filenameresim5;
                    if (yolresim5 != "~/Content/images/resim/urun/resim5/" && _filenameresim5 != null && _filenameresim5 != "")
                    {
                        Request.Files[6].SaveAs(Server.MapPath(yolresim5));
                        urun.resim5 = "/Content/images/resim/urun/resim5/" + _filenameresim5;
                    }
                }
                if (dosyaadiresim6 != null && dosyaadiresim6 != "")
                {
                    string _filenameresim6 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadiresim6;
                    string yolresim6 = "~/Content/images/resim/urun/resim6/" + _filenameresim6;
                    if (yolresim6 != "~/Content/images/resim/urun/resim6/" && _filenameresim6 != null && _filenameresim6 != "")
                    {
                        Request.Files[7].SaveAs(Server.MapPath(yolresim6));
                        urun.resim6 = "/Content/images/resim/urun/resim6/" + _filenameresim6;
                    }

                }

            }

            db.SaveChanges();
            TempData["UrunGuncellendi"] = "Ürün Güncellendi";
            return RedirectToAction("Urunler");
        }

        [Authorize]
        public ActionResult UrunEkle()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }

            ViewModel vw = new ViewModel();

            vw.kategorilers = db.Kategoriler.ToList();

            return View(vw);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UrunEkle(string isim, string aciklama, string urunkodu, string fiyat, string link, string kod, int kategori, Urun kapakresim, Urun aciklamaresim, Urun resim1, Urun resim2, Urun resim3, Urun resim4, Urun resim5, Urun resim6, bool aktif, bool stok)
        {


            var etkin = db.Urun.Where(x => x.urunkodu == urunkodu || x.isim == isim).SingleOrDefault();
            if (etkin != null)
            {
                TempData["UrunMevcut"] = "UrunMevcut";
                return RedirectToAction("UrunEkle");
            }

            if (Proje.Aktif != null)
            {

                Urun u = new Urun()
                {
                    isim = isim,
                    aciklama = aciklama,
                    urunkodu = urunkodu,
                    satin_al = link,
                    fiyat = fiyat,
                    kod = kod,
                    kategoriID = kategori,
                    aktif = aktif,
                    stok = stok,
                    tarih = DateTime.Now,
                    adminID = Proje.Aktif.adminData.adminID

                };

                if (Request.Files.Count > 0)
                {
                    //string newname = Guid.NewGuid().ToString() + Path.GetExtension(kapakresim.FileName);
                    string dosyaadikapakresim = Path.GetFileName(Request.Files[0].FileName);
                    string dosyaadiaciklamaresim = Path.GetFileName(Request.Files[1].FileName);
                    string dosyaadiresim1 = Path.GetFileName(Request.Files[2].FileName);
                    string dosyaadiresim2 = Path.GetFileName(Request.Files[3].FileName);
                    string dosyaadiresim3 = Path.GetFileName(Request.Files[4].FileName);
                    string dosyaadiresim4 = Path.GetFileName(Request.Files[5].FileName);
                    string dosyaadiresim5 = Path.GetFileName(Request.Files[6].FileName);
                    string dosyaadiresim6 = Path.GetFileName(Request.Files[7].FileName);


                    string yolkapakresim = "~/Content/images/resim/urun/kapakresim/" + dosyaadikapakresim;
                    string yolaciklamaresim = "~/Content/images/resim/urun/aciklamaresim/" + dosyaadiaciklamaresim;
                    string yolresim1 = "~/Content/images/resim/urun/resim1/" + dosyaadiresim1;
                    string yolresim2 = "~/Content/images/resim/urun/resim2/" + dosyaadiresim2;
                    string yolresim3 = "~/Content/images/resim/urun/resim3/" + dosyaadiresim3;
                    string yolresim4 = "~/Content/images/resim/urun/resim4/" + dosyaadiresim4;
                    string yolresim5 = "~/Content/images/resim/urun/resim5/" + dosyaadiresim5;
                    string yolresim6 = "~/Content/images/resim/urun/resim6/" + dosyaadiresim6;

                    if (yolkapakresim != "~/Content/images/resim/urun/kapakresim/")
                    {
                        Request.Files[0].SaveAs(Server.MapPath(yolkapakresim));
                        u.kapakresim = "/Content/images/resim/urun/kapakresim/" + dosyaadikapakresim;
                    }
                    if (yolaciklamaresim != "~/Content/images/resim/urun/aciklamaresim/")
                    {
                        Request.Files[1].SaveAs(Server.MapPath(yolaciklamaresim));
                        u.aciklamaresim = "/Content/images/resim/urun/aciklamaresim/" + dosyaadiaciklamaresim;
                    }
                    if (yolresim1 != "~/Content/images/resim/urun/resim1/")
                    {
                        Request.Files[2].SaveAs(Server.MapPath(yolresim1));
                        u.resim1 = "/Content/images/resim/urun/resim1/" + dosyaadiresim1;
                    }
                    if (yolresim2 != "~/Content/images/resim/urun/resim2/")
                    {
                        Request.Files[3].SaveAs(Server.MapPath(yolresim2));
                        u.resim2 = "/Content/images/resim/urun/resim2/" + dosyaadiresim2;
                    }
                    if (yolresim3 != "~/Content/images/resim/urun/resim3/")
                    {
                        Request.Files[4].SaveAs(Server.MapPath(yolresim3));
                        u.resim3 = "/Content/images/resim/urun/resim3/" + dosyaadiresim3;
                    }
                    if (yolresim4 != "~/Content/images/resim/urun/resim4/")
                    {
                        Request.Files[5].SaveAs(Server.MapPath(yolresim4));
                        u.resim4 = "/Content/images/resim/urun/resim4/" + dosyaadiresim4;
                    }
                    if (yolresim5 != "~/Content/images/resim/urun/resim5/")
                    {
                        Request.Files[6].SaveAs(Server.MapPath(yolresim5));
                        u.resim5 = "/Content/images/resim/urun/resim5/" + dosyaadiresim5;
                    }
                    if (yolresim6 != "~/Content/images/resim/urun/resim6/")
                    {
                        Request.Files[7].SaveAs(Server.MapPath(yolresim6));
                        u.resim6 = "/Content/images/resim/urun/resim6/" + dosyaadiresim6;
                    }
                    db.Urun.Add(u);
                    db.SaveChanges();
                    TempData["UrunEklendi"] = "Ürün Eklendi";
                    return RedirectToAction("UrunEkle");
                }


            }


            else
            {
                return RedirectToAction("Login");
            }


            return RedirectToAction("UrunEkle");

        }

        [Authorize]
        public ActionResult Sil(int urunID)
        {
            using (ShoppingContext db = new ShoppingContext())
            {
                var silicnecekurun = db.Urun.Find(urunID);
                if (silicnecekurun == null)
                {
                    return HttpNotFound();
                }
                db.Urun.Remove(silicnecekurun);
                db.SaveChanges();
                TempData["UrunSilindi"] = "Urun Silindi";
                return RedirectToAction("Urunler");

            }
        }


        [Authorize]
        public ActionResult KategoriEkle()
        {
            if (Proje.Aktif.adminData.adi_soyadi == null)
            {
                return RedirectToAction("Login");
            }

            ViewModel vw = new ViewModel();

            vw.kategorilers = db.Kategoriler.ToList();

            return View(vw);
        }

        [Authorize]
        public ActionResult OdemeYontemleri()
        {
            if (Proje.Aktif.adminData.adi_soyadi == null)
            {
                return RedirectToAction("Login");
            }

            SiteAyarViewModel vw = new SiteAyarViewModel();

            vw.odemes = db.Odeme.ToList();

            return View(vw);
        }

        [HttpPost]
        public ActionResult OdemeEkle(string banka, string iban, bool aktif)
        {


            if (Proje.Aktif.adminData.adi_soyadi != null)
            {

                Odeme odeme = new Odeme()

                {
                    banka = banka,
                    iban = iban,
                    aktif = aktif


                };
                db.Odeme.Add(odeme);
                db.SaveChanges();
                TempData["Odemeeklendi"] = "Odeme Eklendi";
                return RedirectToAction("OdemeYontemleri");
            }
            else
            {
                return RedirectToAction("Login");
            }

           


        }

        [HttpPost]
        [Authorize]
        public ActionResult OdemeDuzenle(int odemeID, string banka, string iban, bool aktif)
        {


            Odeme odeme = db.Odeme.Where(x => x.odemeID == odemeID).FirstOrDefault();

            odeme.banka = banka;
            odeme.iban = iban;
            odeme.aktif = aktif;

            db.SaveChanges();
            TempData["Odemeguncellendi"] = "Odeme Guncellendi";
            return RedirectToAction("OdemeYontemleri");
        }

        public ActionResult Odemesil(int odemeID)
        {
            using (ShoppingContext db = new ShoppingContext())
            {
                var silinecekodeme = db.Odeme.Find(odemeID);
                if (silinecekodeme == null)
                {
                    return HttpNotFound();
                }
                db.Odeme.Remove(silinecekodeme);
                db.Odeme.Remove(silinecekodeme);
                db.SaveChanges();
                TempData["Odemesilindi"] = "Odeme Silindi";
                return RedirectToAction("OdemeYontemleri");
            }

        }


        [Authorize]
        [HttpPost]
        public ActionResult KategoriEkle(string adi, string etiket, bool aktif)
        {


            var etkin = db.Kategoriler.Where(x => x.adi == adi || x.etiket == etiket).SingleOrDefault();
            if (etkin != null)
            {
                TempData["KategoriMevcut"] = "KategoriMevcut";
            }

            if (Proje.Aktif.adminData.adi_soyadi != null)
            {

                Kategoriler k = new Kategoriler()

                {
                    adi = adi,
                    etiket = etiket,
                    aktif = aktif,

                };


                db.Kategoriler.Add(k);
                db.SaveChanges();
                TempData["KategoriEklendi"] = "Kategori Eklendi";
                return RedirectToAction("KategoriEkle");




            }
            else
            {
                return RedirectToAction("Login");
            }



        }

        [Authorize]
        public ActionResult KategoriDuzenle(int kategoriID, string adi, string etiket, bool aktif)
        {
            Kategoriler kategori = db.Kategoriler.Where(x => x.kategoriID == kategoriID).FirstOrDefault();

            kategori.adi = adi;
            kategori.etiket = etiket;
            kategori.aktif = aktif;

            db.SaveChanges();
            TempData["KategoriGuncellendi"] = "Kategori Guncellendi";
            return RedirectToAction("Kategori");

        }

        [Authorize]
        public ActionResult KategoriSil(int kategoriID)
        {
            using (ShoppingContext db = new ShoppingContext())
            {
                var silicnecekKategori = db.Kategoriler.Find(kategoriID);
                if (silicnecekKategori == null)
                {
                    return HttpNotFound();
                }
                db.Kategoriler.Remove(silicnecekKategori);
                db.SaveChanges();
                TempData["KategoriSilindi"] = "Kategori Silindi";
                return RedirectToAction("Kategori");

            }
        }

        [Authorize]
        public ActionResult Kategori()
        {

            ViewModel vw = new ViewModel();

            vw.kategorilers = db.Kategoriler.ToList();

            return View(vw);
        }

        [HttpPost]
        public ActionResult AdminEkle(string adi_soyadi, string sifre)
        {
            ViewModel vw = new ViewModel();

            var yon = db.Admin.Where(x => x.adi_soyadi == adi_soyadi).SingleOrDefault();
            if (yon != null)
            {
                TempData["AdminEklenmedi"] = "AdminEklenmedi";
            }
            else
            {
                Admin a = new Admin()
                {
                    adi_soyadi = adi_soyadi,
                    sifre = sifre

                };
                db.Admin.Add(a);
                db.SaveChanges();
                TempData["AdminEklendi"] = "AdminEklendi";
                return RedirectToAction("AdminDuzenle");
            }

            return RedirectToAction("AdminDuzenle");

        }

        [Authorize]
        public ActionResult AdminDuzenle()
        {
            ViewModel vw = new ViewModel();

            vw.admins = db.Admin.ToList();
            return View(vw);
        }

        public ActionResult AdminSil(int adminID)
        {
            using (ShoppingContext db = new ShoppingContext())
            {
                var silinecekadmin = db.Admin.Find(adminID);
                if (silinecekadmin == null)
                {
                    return HttpNotFound();
                }
                db.Admin.Remove(silinecekadmin);
                db.Admin.Remove(silinecekadmin);
                db.SaveChanges();
                TempData["AdminSilindi"] = "Yonetici Silindi";
                return RedirectToAction("AdminDuzenle");
            }

        }

        [Authorize]
        public ActionResult SiteAyar(int id)
        {
            try
            {
                id = 1;
                SiteAyarViewModel model = new SiteAyarViewModel();
                model.siteAyar = db.siteAyar.Where(x => x.siteayarID == id).FirstOrDefault();


                return View(model);

            }
            catch (Exception)
            {
                ViewBag.Message = "Bilinmeyen bir hata meydana geldi";
                return View();
            }
        }


        [HttpPost]
        public ActionResult Siteduzenle(int siteayarID, string site_adi, string logo_yazi, siteAyar logo, siteAyar katalog, siteAyar background, bool kayarlogo_aktif, string mail, string telefon1, string telefon2, string telefon3, string harita, string adres, string hakkimizda_baslik, string hakkimizda, siteAyar hakkimizda_resim, siteAyar galeri_resim1, siteAyar galeri_resim2, siteAyar galeri_resim3, siteAyar galeri_resim4, string yazi1, string yazi2, string yazi3, string iletisim_saat,string iletisim_haftaici, string iletisim_cumartesi, string iletisim_pazar, string _abstract, string description, string keywords)
        {
            siteAyar ayar = db.siteAyar.Where(x => x.siteayarID == siteayarID).FirstOrDefault();

            
            ayar.site_adi = site_adi;
            ayar.logo_yazi = logo_yazi;
            ayar.kayarlogo_aktif = kayarlogo_aktif;
            ayar.mail = mail;
            ayar.harita = harita;
            ayar.telefon1 = telefon1;
            ayar.telefon2 = telefon2;
            ayar.telefon3 = telefon3;
            ayar.harita = harita;
            ayar.adres = adres;
            ayar.hakkimizda_baslik = hakkimizda_baslik;
            ayar.hakkimizda = hakkimizda;
            ayar.yazi1 = yazi1;
            ayar.yazi2 = yazi2;
            ayar.yazi3 = yazi3;
            ayar.iletisim_saat = iletisim_saat;
            ayar.iletisim_haftaici = iletisim_haftaici;
            ayar.iletisim_cumartesi = iletisim_cumartesi;
            ayar.iletisim_pazar = iletisim_pazar;
            ayar._abstract = _abstract;
            ayar.description = description;
            ayar.keywords = keywords;

            if (Request.Files.Count > 0)
            {
                string dosyaadilogo = Path.GetFileName(Request.Files[0].FileName);
                string dosyaadibackground = Path.GetFileName(Request.Files[1].FileName);
                string dosyaadikatalog = Path.GetFileName(Request.Files[2].FileName);
                string dosyaadihakkimizda_resim = Path.GetFileName(Request.Files[3].FileName);
                string dosyaadigaleri_resim1 = Path.GetFileName(Request.Files[4].FileName);
                string dosyaadigaleri_resim2 = Path.GetFileName(Request.Files[5].FileName);
                string dosyaadigaleri_resim3 = Path.GetFileName(Request.Files[6].FileName);
                string dosyaadigaleri_resim4 = Path.GetFileName(Request.Files[7].FileName);


                if (dosyaadilogo != null && dosyaadilogo != "")
                {
                    string _filenamelogo = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadilogo;
                    string yollogo = "~/Content/images/resim/siteayar/logo/" + _filenamelogo;
                    if (yollogo != "~/Content/images/resim/siteayar/logo/" && _filenamelogo != null && _filenamelogo != "")
                    {
                        Request.Files[0].SaveAs(Server.MapPath(yollogo));
                        ayar.logo = "/Content/images/resim/siteayar/logo/" + _filenamelogo;
                    }

                }
                if (dosyaadibackground != null && dosyaadibackground != "")
                {
                    string _filenamebackground = DateTime.Now.ToString("yyyyMMd_Hms_fff") + dosyaadibackground;
                    string yolbackground = "~/Content/images/resim/siteayar/background/" + _filenamebackground;
                    if (yolbackground != "~/Content/images/resim/siteayar/background/" && _filenamebackground != null && _filenamebackground != "")
                    {
                        Request.Files[1].SaveAs(Server.MapPath(yolbackground));
                        ayar.background = "/Content/images/resim/siteayar/background/" + _filenamebackground;
                    }

                }
                
                if (dosyaadikatalog != null && dosyaadikatalog != "")
                {
                    string _filenamekatalog = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadikatalog;
                    string yolkatalog = "~/Content/Dosya/" + _filenamekatalog;
                    if (yolkatalog != "~/Content/Dosya/" && _filenamekatalog != null && _filenamekatalog != "")
                    {
                        Request.Files[2].SaveAs(Server.MapPath(yolkatalog));
                        ayar.katalog = "/Content/Dosya/" + _filenamekatalog;
                    }

                }

                if (dosyaadihakkimizda_resim != null && dosyaadihakkimizda_resim != "")
                {
                    string _filenamehakkimizda_resim = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadihakkimizda_resim;
                    string yolhakkimizda_resim = "~/Content/images/resim/siteayar/hakkimizdaresim/" + _filenamehakkimizda_resim;
                    if (yolhakkimizda_resim != "~/Content/images/resim/siteayar/hakkimizdaresim/" && _filenamehakkimizda_resim != null && _filenamehakkimizda_resim != "")
                    {
                        Request.Files[3].SaveAs(Server.MapPath(yolhakkimizda_resim));
                        ayar.hakkimizda_resim = "/Content/images/resim/siteayar/hakkimizdaresim/" + _filenamehakkimizda_resim;
                    }

                }

                if (dosyaadigaleri_resim1 != null && dosyaadigaleri_resim1 != "")
                {
                    string _filenamegaleri_resim1 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadigaleri_resim1;
                    string yolgaleri_resim1 = "~/Content/images/resim/siteayar/galeriresim1/" + _filenamegaleri_resim1;
                    if (yolgaleri_resim1 != "~/Content/images/resim/siteayar/galeriresim1/" && _filenamegaleri_resim1 != null && _filenamegaleri_resim1 != "")
                    {
                        Request.Files[4].SaveAs(Server.MapPath(yolgaleri_resim1));
                        ayar.galeri_resim1 = "/Content/images/resim/siteayar/galeriresim1/" + _filenamegaleri_resim1;
                    }

                }

                if (dosyaadigaleri_resim2 != null && dosyaadigaleri_resim2 != "")
                {
                    string _filenamegaleri_resim2 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadigaleri_resim2;
                    string yolgaleri_resim2 = "~/Content/images/resim/siteayar/galeriresim2/" + _filenamegaleri_resim2;
                    if (yolgaleri_resim2 != "~/Content/images/resim/siteayar/galeriresim2/" && _filenamegaleri_resim2 != null && _filenamegaleri_resim2 != "")
                    {
                        Request.Files[5].SaveAs(Server.MapPath(yolgaleri_resim2));
                        ayar.galeri_resim2 = "/Content/images/resim/siteayar/galeriresim2/" + _filenamegaleri_resim2;
                    }
                }

                if (dosyaadigaleri_resim3 != null && dosyaadigaleri_resim3 != "")
                {
                    string _filenamegaleri_resim3 = DateTime.Now.ToString("(yyyyMMd_Hms_fff)") + dosyaadigaleri_resim3;
                    string yolgaleri_resim3 = "~/Content/images/resim/siteayar/galeriresim3/" + _filenamegaleri_resim3;
                    if (yolgaleri_resim3 != "~/Content/images/resim/siteayar/galeriresim3/" && _filenamegaleri_resim3 != null && _filenamegaleri_resim3 != "")
                    {
                        Request.Files[6].SaveAs(Server.MapPath(yolgaleri_resim3));
                        ayar.galeri_resim3 = "/Content/images/resim/siteayar/galeriresim3/" + _filenamegaleri_resim3;
                    }
                }

                if (dosyaadigaleri_resim4 != null && dosyaadigaleri_resim4 != "")
                {
                    string _filenamegaleri_resim4 = DateTime.Now.ToString("yyyyMMd_Hms_fff") + dosyaadigaleri_resim4;
                    string yolgaleri_resim4 = "~/Content/images/resim/siteayar/galeriresim4/" + _filenamegaleri_resim4;
                    if (yolgaleri_resim4 != "~/Content/images/resim/siteayar/galeriresim4/" && _filenamegaleri_resim4 != null && _filenamegaleri_resim4 != "")
                    {
                        Request.Files[7].SaveAs(Server.MapPath(yolgaleri_resim4));
                        ayar.galeri_resim4 = "/Content/images/resim/siteayar/galeriresim4/" + _filenamegaleri_resim4;
                    }
                }

               
            }



            db.SaveChanges();
            TempData["Kaydedildi"] = "Kaydedildi";
            return RedirectToAction("SiteAyar/1");
        }


        [Authorize]
        public ActionResult Slider()
        {
            ViewModel vw = new ViewModel();

            vw.sliders = db.Slider.ToList();

            return View(vw);
        }

        [HttpPost]
        public ActionResult SliderEkle(string slider_baslik, string slider_yazi, Slider resim, bool aktif)
        {


            if (Proje.Aktif != null)
            {

                Slider slider = new Slider()

                {
                    slider_baslik = slider_baslik,
                    slider_yazi = slider_yazi,
                    aktif = aktif


                };

                if (Request.Files.Count > 0)
                {
                    string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                    string _filename = DateTime.Now.ToString("yyyyMMd_Hms_fff") + dosyaadi;
                    string yol = "~/Content/images/resim/siteayar/sliderresim/" + _filename;


                    if (yol == "~/Content/images/resim/siteayar/sliderresim/")
                    {
                        TempData["ResimHata"] = "ResimHata";
                    }
                    else
                    {
                        Request.Files[0].SaveAs(Server.MapPath(yol));
                        slider.resim = "/Content/images/resim/siteayar/sliderresim/" + _filename;
                        db.Slider.Add(slider);
                        db.SaveChanges();
                        TempData["SliderEklendi"] = "Slider Eklendi";
                    }


                }
                else
                {
                    TempData["ResimEkle"] = "Resim Ekleyiniz";

                }

            }
            else
            {
                return RedirectToAction("Login");
            }


            return RedirectToAction("Slider");


        }


        [HttpPost]
        [Authorize]
        public ActionResult SliderDuzenle(int sliderID, string slider_baslik, string slider_yazi, Slider resim, bool aktif)
        {


            Slider slider = db.Slider.Where(x => x.sliderID == sliderID).FirstOrDefault();

            slider.slider_baslik = slider_baslik;
            slider.slider_yazi = slider_yazi;
            slider.aktif = aktif;


            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string _filename = DateTime.Now.ToString("yyyyMMd_Hms_fff") + dosyaadi;
                string yol = "~/Content/images/resim/siteayar/sliderresim/" + _filename;
                if (yol == "~/Content/images/resim/siteayar/sliderresim/")
                {
                    TempData["ResimHata"] = "ResimHata";

                }
                else if (dosyaadi != null && dosyaadi != "" )
                {
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    slider.resim = "/Content/images/resim/siteayar/sliderresim/" + _filename;
                   
                }
                db.SaveChanges();
                TempData["SliderGuncellendi"] = "Slider Güncellendi";
            }

            
            return RedirectToAction("Slider");
        }

        public ActionResult Slidersil(int sliderID)
        {
            using (ShoppingContext db = new ShoppingContext())
            {
                var silinecekslider = db.Slider.Find(sliderID);
                if (silinecekslider == null)
                {
                    return HttpNotFound();
                }
                db.Slider.Remove(silinecekslider);
                db.Slider.Remove(silinecekslider);
                db.SaveChanges();
                TempData["SliderSilindi"] = "Slider Silindi";
                return RedirectToAction("Slider");
            }

        }


    }
}

