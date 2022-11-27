namespace shopping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        public int sliderID { get; set; }

        [Column(TypeName = "text")]
        public string slider_baslik { get; set; }

        [Column(TypeName = "text")]
        public string slider_yazi { get; set; }

        [Column(TypeName = "text")]
        public string resim { get; set; }

        public bool? aktif { get; set; }
    }
}
