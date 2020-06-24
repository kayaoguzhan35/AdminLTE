namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dosya")]
    public partial class Dosya
    {
        public int id { get; set; }

        [Required]
        public string link { get; set; }
    }
}
