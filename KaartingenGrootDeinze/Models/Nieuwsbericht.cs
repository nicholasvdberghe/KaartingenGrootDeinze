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
        [Required(ErrorMessage="Geef een titel op.")]
        public string Titel { get; set; }
        [Required(ErrorMessage = "Vul het bericht in.")]
        public string Inhoud { get; set; }
        [Required(ErrorMessage = "Geef een datum op.")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yy}")]
        public DateTime Datum { get; set; }

        public int? KaartingId { get; set; }
        public virtual Kaarting Kaarting { get; set; }
    }
}