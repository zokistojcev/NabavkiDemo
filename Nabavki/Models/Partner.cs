namespace Nabavki.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Partner")]
    public partial class Partner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partner()
        {
            Artiklis = new HashSet<Artikli>();
        }

        [Key]
        public int IdPartner { get; set; }

        [StringLength(150)]
        public string Naziv { get; set; }

        public int IdFirma { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Artikli> Artiklis { get; set; }

        public virtual Firma Firma { get; set; }
    }
}
