namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GaleriTipi")]
    public partial class GaleriTipi
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string ad { get; set; }

        public int? ustId { get; set; }
    }
}
