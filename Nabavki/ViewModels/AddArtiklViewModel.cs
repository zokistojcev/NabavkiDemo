using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nabavki.ViewModels
{
    public class AddArtiklViewModel
    {
        [Key]
        public int IdArtikli { get; set; }

        public int IdPartner { get; set; }

        public List<FirmaViewModel> Firmi { get; set; }
        [Required]
        public string ArtiklSifra { get; set; }
        [Required]
        public string ArtiklNaziv { get; set; }

    }
}