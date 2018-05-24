using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nabavki.ViewModels
{
    public class PartnerViewModel
    {
        [Key]
        public int IdPartner { get; set; }

        [Required]
        [StringLength(150)]
        public string Naziv { get; set; }

        public int IdFirma { get; set; }

    }
}