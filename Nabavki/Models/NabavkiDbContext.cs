namespace Nabavki.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NabavkiDbContext : ApplicationDbContext
    {

        public virtual DbSet<Artikli> Artikli { get; set; }
        public virtual DbSet<Firma> Firmi { get; set; }
        public virtual DbSet<Partner> Partneri { get; set; }

    }
}
