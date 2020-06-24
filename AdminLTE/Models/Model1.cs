namespace AdminLTE.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model110")
        {
        }

        public virtual DbSet<Dosya> Dosyas { get; set; }
        public virtual DbSet<Galeriler> Galerilers { get; set; }
        public virtual DbSet<GaleriResim> GaleriResims { get; set; }
        public virtual DbSet<GaleriTipi> GaleriTipis { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Menuler> Menulers { get; set; }
        public virtual DbSet<MenuTipi> MenuTipis { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Urunler> Urunlers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Galeriler>()
                .HasMany(e => e.GaleriResims)
                .WithOptional(e => e.Galeriler)
                .WillCascadeOnDelete();
        }
    }
}
