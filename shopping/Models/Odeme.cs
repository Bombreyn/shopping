namespace shopping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Odeme")]
    public partial class Odeme
    {
        public int odemeID { get; set; }

        [Column(TypeName = "text")]
        public string banka { get; set; }

        [Column(TypeName = "text")]
        public string iban { get; set; }

        public bool? aktif { get; set; }
    }
}
