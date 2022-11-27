namespace shopping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("siteAyar")]
    public partial class siteAyar
    {
        public int siteayarID { get; set; }

        [StringLength(50)]
        public string site_adi { get; set; }

        [Column(TypeName = "text")]
        public string katalog { get; set; }

        [StringLength(50)]
        public string logo_yazi { get; set; }

        [Column(TypeName = "text")]
        public string logo { get; set; }

        public bool? kayarlogo_aktif { get; set; }

        [StringLength(50)]
        public string mail { get; set; }

        [StringLength(50)]
        public string telefon1 { get; set; }

        [StringLength(50)]
        public string telefon2 { get; set; }

        [StringLength(50)]
        public string telefon3 { get; set; }

        [Column(TypeName = "text")]
        public string harita { get; set; }

        [Column(TypeName = "text")]
        public string adres { get; set; }

        [Column(TypeName = "text")]
        public string hakkimizda_baslik { get; set; }

        [Column(TypeName = "text")]
        public string hakkimizda { get; set; }

        [Column(TypeName = "text")]
        public string hakkimizda_resim { get; set; }

        [Column(TypeName = "text")]
        public string galeri_resim1 { get; set; }

        [Column(TypeName = "text")]
        public string galeri_resim2 { get; set; }

        [Column(TypeName = "text")]
        public string galeri_resim3 { get; set; }

        [Column(TypeName = "text")]
        public string galeri_resim4 { get; set; }

        [Column(TypeName = "text")]
        public string background { get; set; }

        [Column(TypeName = "text")]
        public string yazi1 { get; set; }

        [Column(TypeName = "text")]
        public string yazi2 { get; set; }

        [Column(TypeName = "text")]
        public string yazi3 { get; set; }

        [Column(TypeName = "text")]
        public string iletisim_saat { get; set; }

        [Column(TypeName = "text")]
        public string iletisim_haftaici { get; set; }

        [Column(TypeName = "text")]
        public string iletisim_cumartesi { get; set; }

        [Column(TypeName = "text")]
        public string iletisim_pazar { get; set; }

        [Column("abstract", TypeName = "text")]
        public string _abstract { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        [Column(TypeName = "text")]
        public string keywords { get; set; }
    }
}
