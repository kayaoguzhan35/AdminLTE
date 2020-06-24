namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GaleriResim")]
    public partial class GaleriResim
    {
        public int id { get; set; }

        [Required]
        public string link { get; set; }

        public int? galerilerId { get; set; }

        public int? urunlerId { get; set; }

        public string kucukLink { get; set; }

        public virtual Galeriler Galeriler { get; set; }

        public virtual Urunler Urunler { get; set; }
    }
}
