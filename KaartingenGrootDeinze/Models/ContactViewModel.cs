using System;
using System.ComponentModel.DataAnnotations;

namespace KaartingenGrootDeinze.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage ="{0} is verplicht")]
        public string Afzender { get; set; }
        [Required(ErrorMessage = "{0} is verplicht")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Geef een geldig e-mailadres op")]
        public string Emailadres { get; set; }
        [Required(ErrorMessage = "{0} is verplicht")]
        public string Onderwerp { get; set; }
        [Required(ErrorMessage = "{0} is verplicht")]
        [DataType(DataType.MultilineText)]
        public string Boodschap { get; set; }
    }
}