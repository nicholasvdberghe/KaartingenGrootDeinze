using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KaartingenGrootDeinze.Models
{
    [Table("Zaken")]
    public class Zaak
    {
        public int ZaakId { get; set; }
        [Required(ErrorMessage = "Zaak is verplicht.")]
        public string Naam { get; set; }
        public string Plaats { get; set; }

        [NotMapped]
        public ICollection<Kaarting> Kaartingen { get; set; }
    }
}