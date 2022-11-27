using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace shopping.Models
{
    public partial class ShoppingContext : DbContext
    {
        public ShoppingContext()
            : base("name=ShoppingContext")
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Kategoriler> Kategoriler { get; set; }
        public virtual DbSet<Odeme> Odeme { get; set; }
        public virtual DbSet<siteAyar> siteAyar { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Odeme>()
                .Property(e => e.banka)
                .IsUnicode(false);

            modelBuilder.Entity<Odeme>()
                .Property(e => e.iban)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.katalog)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.harita)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.adres)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.hakkimizda_baslik)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.hakkimizda)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.hakkimizda_resim)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.galeri_resim1)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.galeri_resim2)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.galeri_resim3)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.galeri_resim4)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.background)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.yazi1)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.yazi2)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.yazi3)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.iletisim_saat)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.iletisim_haftaici)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.iletisim_cumartesi)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.iletisim_pazar)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e._abstract)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<siteAyar>()
                .Property(e => e.keywords)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.slider_baslik)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.slider_yazi)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.resim)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.satin_al)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.kapakresim)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.resim1)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.resim2)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.resim3)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.resim4)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.resim5)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.resim6)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.aciklama)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.aciklamaresim)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.kod)
                .IsUnicode(false);
        }
    }
}
