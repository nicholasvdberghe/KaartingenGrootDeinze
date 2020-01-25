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
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string Titel { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string Inhoud { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is verplicht.")]
        public DateTime Datum { get; set; }
    }
}