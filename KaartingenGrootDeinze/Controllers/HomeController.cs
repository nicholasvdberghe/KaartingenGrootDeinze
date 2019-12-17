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

        public ActionResult Index()
        {
            var eindDatum = DateTime.Now.AddDays(21);
            List<Kaarting> kaartingen = kaartingService.GetToekomstigeKaartingen(eindDatum);
            //ViewBag.Bericht = berichtService.GetLaatsteNieuwsbericht().Inhoud;            
            //List<Nieuwsbericht> berichten = berichtService.GetRecenteNieuwsberichten();
            //ViewBag.Berichten = berichten;
            return View(kaartingen);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}