namespace shopping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urun")]
    public partial class Urun
    {
        public int urunID { get; set; }

        [Column(TypeName = "text")]
        public string satin_al { get; set; }

        [StringLength(250)]
        public string isim { get; set; }

        [Column(TypeName = "text")]
        public string kapakresim { get; set; }

        [Column(TypeName = "text")]
        public string resim1 { get; set; }

        [Column(TypeName = "text")]
        public string resim2 { get; set; }

        [Column(TypeName = "text")]
        public string resim3 { get; set; }

        [Column(TypeName = "text")]
        public string resim4 { get; set; }

        [Column(TypeName = "text")]
        public string resim5 { get; set; }

        [Column(TypeName = "text")]
        public string resim6 { get; set; }

        [Column(TypeName = "text")]
        public string aciklama { get; set; }

        [Column(TypeName = "text")]
        public string aciklamaresim { get; set; }

        [StringLength(50)]
        public string urunkodu { get; set; }

        [Column(TypeName = "text")]
        public string kod { get; set; }

        public bool? aktif { get; set; }

        public DateTime? tarih { get; set; }

        [StringLength(50)]
        public string fiyat { get; set; }

        public bool? stok { get; set; }

        public int? adminID { get; set; }

        public int? kategoriID { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Kategoriler Kategoriler { get; set; }
    }
}
