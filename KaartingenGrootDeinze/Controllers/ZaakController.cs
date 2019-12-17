using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaartingenGrootDeinze.Models;
using KaartingenGrootDeinze.Services;

namespace KaartingenGrootDeinze.Controllers
{
    [RoutePrefix("Zaken")]
    public class ZaakController : Controller
    {
        private KaartingService kaartingService = new KaartingService();
        private ZaakService zaakService = new ZaakService();

        [Route]
        public ActionResult Index()
        {
            List<Zaak> zaken = zaakService.GetAlleZaken();
            return View(zaken);
        }

        [Route("Toevoegen")]
        public ActionResult ToevoegenForm()
        {
            return View(new Zaak());
        }

        [HttpPost]
        public ActionResult Toevoegen(Zaak zaak)
        {
            if (ModelState.IsValid)
            {
                zaakService.Insert(zaak);
                return RedirectToAction("Index", "Zaak");
            }
            else
            {
                return View("ToevoegenForm", "Zaak");
            }
        }

        [Route("Wijzigen/{id}")]
        public ActionResult WijzigenForm(int id)
        {
            return View(zaakService.GetZaakById(id));
        }
     
        [HttpPost]
        public ActionResult Wijzigen(Zaak nieuweZaak)
        {
            if (ModelState.IsValid)
            {
                zaakService.Update(nieuweZaak);
                return RedirectToAction("Index");
            }
            else
            {
                return View(nieuweZaak);
            }
        }

        [Route("Verwijderen/{id}")]
        public ActionResult VerwijderenForm(int id)
        {
            Zaak zaak = zaakService.GetZaakById(id);
            return View(zaak);
        }

        public ActionResult Verwijderen(int id)
        {
            zaakService.Delete(id);
            return RedirectToAction("Index");

        }
    }
}