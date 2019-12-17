using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KaartingenGrootDeinze.Models
{
    [Table("Nieuwsberichten")]
    public class Nieuwsbericht
    {
        public int NieuwsberichtId { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public DateTime Datum { get; set; }

        public int KaartingId { get; set; }
        public Kaarting Kaarting { get; set; }
    }
}