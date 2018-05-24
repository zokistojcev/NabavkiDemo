namespace Nabavki.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Artikli")]
    public partial class Artikli
    {
        [Key]
        public int IdArtikli { get; set; }

        [StringLength(50)]
        public string Sifra { get; set; }

        [StringLength(150)]
        public string Naziv { get; set; }

        public int IdPartner { get; set; }
    }
}
