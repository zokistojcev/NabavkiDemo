using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nabavki.ViewModels
{
    public class ArtiklViewModel
    {
        [Key]
        public int IdArtikli { get; set; }

        [Required]
        [StringLength(50)]
        public string Sifra { get; set; }

        [Required]
        [StringLength(150)]
        public string Naziv { get; set; }

        public int IdPartner { get; set; }
    }
}