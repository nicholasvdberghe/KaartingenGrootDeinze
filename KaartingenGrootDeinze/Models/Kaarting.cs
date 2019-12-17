using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaartingenGrootDeinze.Models
{
    [Table("Kaartingen")]
    public class Kaarting
    {
        public int KaartingId { get; set; }
        [DisplayFormat(DataFormatString = "{0: dddd dd/MM/yy}")]
        public DateTime Datum { get; set; }
        [DisplayFormat(DataFormatString = "{0: € ##0}")]
        public int Prijzengeld { get; set; }
        [DisplayFormat(DataFormatString = "{0: HH.mm uur}")]
        public DateTime Startuur { get; set; }

        public int ZaakId { get; set; }
        public virtual Zaak Zaak { get; set; }

        //public ICollection<Nieuwsbericht> Nieuwsberichten { get; set; }
    }
}