using Nabavki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nabavki.ViewModels
{
    public class FirmaViewModel
    {

        public int IdFirma { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Naziv { get; set; }

        public List<PartnerViewModel> Partneri { get; set; }

        public FirmaViewModel()
        {
            Partneri = new List<PartnerViewModel>();
        }

        public int BrojNaPartneri { get; set; }

    }
}