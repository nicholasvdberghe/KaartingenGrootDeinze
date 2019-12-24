using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaartingenGrootDeinze.Models
{
    public class NieuwsberichtViewModel
    {
        public int NieuwsberichtId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Geef een titel op.")]
        public string Titel { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Vul het bericht in.")]
        public string Inhoud { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Geef een datum op.")]
        public DateTime Datum { get; set; }

        [Display(Name = "Kaarting")]
        public int? KaartingId { get; set; }
        [Display(Name = "Kaarting")]
        public List<Kaarting> Kaartingen { get; set; }
    }
}