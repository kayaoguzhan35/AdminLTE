namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string ad { get; set; }

        public bool aktif { get; set; }

        public int ustId { get; set; }

        public int sira { get; set; }

        [Required]
        [StringLength(50)]
        public string icerik { get; set; }

        [StringLength(50)]
        public string dislink { get; set; }
    }
}
