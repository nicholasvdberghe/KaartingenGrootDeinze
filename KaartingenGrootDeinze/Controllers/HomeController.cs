using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaartingenGrootDeinze.Services;
using KaartingenGrootDeinze.Models;

namespace KaartingenGrootDeinze.Controllers
{
    public class HomeController : Controller
    {
        private KaartingService kaartingService = new KaartingService();
        private NieuwsberichtService nieuwsberichtService = new NieuwsberichtService();

        public ActionResult Index()
        {
            var ondergrens = DateTime.Today;
            var bovengrens = DateTime.Now.AddDays(21);
            List<Kaarting> kaartingen = kaartingService.GetGefilterdeKaartingen(ondergrens, bovengrens);
            string laatsteBericht = nieuwsberichtService.GetAlleNieuwsberichten().Last().Inhoud;
            ViewBag.Bericht = laatsteBericht;
            return View(kaartingen);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}