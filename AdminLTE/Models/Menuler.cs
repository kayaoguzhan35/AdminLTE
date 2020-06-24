namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menuler")]
    public partial class Menuler
    {
        public int id { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(50)]
        public string renk { get; set; }
    }
}
