namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Galeriler")]
    public partial class Galeriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Galeriler()
        {
            GaleriResims = new HashSet<GaleriResim>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string ad { get; set; }

        [Required]
        [StringLength(50)]
        public string tip { get; set; }

        public int genislik { get; set; }

        public int yukseklik { get; set; }

        public bool aktif { get; set; }

        [StringLength(50)]
        public string sema { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GaleriResim> GaleriResims { get; set; }
    }
}
