using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaartingenGrootDeinze.Services;
using KaartingenGrootDeinze.Models;
using System.Text;
using System.Threading;

namespace KaartingenGrootDeinze.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private KaartingService kaartingService = new KaartingService();
        private NieuwsberichtService nieuwsberichtService = new NieuwsberichtService();
        private ContactService contactService = new ContactService();

        public ActionResult Index()
        {
            var ondergrens = DateTime.Today;
            var bovengrens = DateTime.Now.AddDays(21);
            List<Kaarting> kaartingen = kaartingService.GetGefilterdeKaartingen(ondergrens, bovengrens);
            string laatsteBericht = nieuwsberichtService.GetAlleNieuwsberichten().Last().Inhoud;
            ViewBag.Bericht = laatsteBericht;
            return View(kaartingen);
        }

        [Route("Contact")]
        public ActionResult ContactForm()
        {
            ContactViewModel vm = new ContactViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Contacteren(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //prepare email
                var afzender = vm.Afzender;
                var afzenderAdres = vm.Emailadres;
                var vanAdres = "nicholasvdberghe@hotmail.com";
                var naarAdres = "gaby.van.den.berghe@hotmail.com";
                var onderwerp = "Nieuw bericht Kaartingen Deinze: " + vm.Onderwerp;
                var boodschap = new StringBuilder();
                boodschap.Append("Naam: " + vm.Afzender + "<br/>");
                boodschap.Append("E-mail: " + vm.Emailadres + "<br/>");
                boodschap.Append("Onderwerp: " + vm.Onderwerp + "<br/>");
                boodschap.Append("Boodschap: " + vm.Boodschap);

                //start email thread
                var tEmail = new Thread(() => contactService.VerstuurEmail(afzender, afzenderAdres, vanAdres, naarAdres, onderwerp, boodschap.ToString()));
                tEmail.Start();
            }
            return View("Bedankt");
        }

    }
}