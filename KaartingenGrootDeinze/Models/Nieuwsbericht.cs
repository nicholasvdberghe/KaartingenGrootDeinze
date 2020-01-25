using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KaartingenGrootDeinze.Models
{
    [Table("Nieuwsberichten")]
    public class Nieuwsbericht
    {
        public Nieuwsbericht()
        {
            Datum = DateTime.Now;
        }

        public int NieuwsberichtId { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        [DisplayFormat(DataFormatString = "{0: dd/MM/yy}")]
        public DateTime Datum { get; set; }
    }
}