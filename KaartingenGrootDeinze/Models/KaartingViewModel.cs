using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KaartingenGrootDeinze.Models
{
    public class KaartingViewModel
    {
        public int KaartingId { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is verplicht")]
        public DateTime Datum { get; set; }
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "{0} is verplicht.")]
        public DateTime Startuur { get; set; }
        [Range(100, 1000, ErrorMessage = "{0} moet tussen de {1} en {2} EUR liggen.")]
        [Required(ErrorMessage = "{0} is verplicht.")]
        public int Prijzengeld { get; set; }

        [Display(Name = "Zaak")]
        public int ZaakId { get; set; }
        [Display(Name = "Zaak")]
        public List<Zaak> Zaken { get; set; }
    }
}