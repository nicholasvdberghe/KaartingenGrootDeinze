using KaartingenGrootDeinze.Models;
using KaartingenGrootDeinze.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KaartingenGrootDeinze.Controllers
{
    [RoutePrefix("Nieuwsberichten")]
    public class NieuwsberichtController : Controller
    {
        private KaartingService kaartingService = new KaartingService();
        private ZaakService zaakService = new ZaakService();
        private NieuwsberichtService nieuwsberichtService = new NieuwsberichtService();

        [Route]
        public ActionResult Index()
        {
            List<Nieuwsbericht> berichten = nieuwsberichtService.GetAlleNieuwsberichten();
            return View(berichten);
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            Nieuwsbericht nb = new Nieuwsbericht();
            nb = nieuwsberichtService.GetNieuwsberichtById(id);
            return View(nb);
        }

        [Route("Toevoegen")]
        public ActionResult ToevoegenForm()
        {
            DateTime ondergrens = DateTime.Now.AddDays(-8);
            var viewModel = new NieuwsberichtViewModel
            {
                Datum = new Nieuwsbericht().Datum,
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Toevoegen(NieuwsberichtViewModel form)
        {
            if (ModelState.IsValid)
            {
                Nieuwsbericht nb = new Nieuwsbericht
                {
                    Titel = form.Titel,
                    Inhoud = form.Inhoud,
                };
                nieuwsberichtService.Insert(nb);
                return RedirectToAction("Index");
            }
            else
            {
                return View(form);
            }
        }

        [Route("Wijzigen/{id}")]
        public ActionResult WijzigenForm(int id)
        {
            DateTime ondergrens = DateTime.Now.AddDays(-8);
            Nieuwsbericht bericht = nieuwsberichtService.GetNieuwsberichtById(id);
            NieuwsberichtViewModel viewModel = new NieuwsberichtViewModel
            {
                Datum = bericht.Datum,
                Inhoud = bericht.Inhoud,
                Titel = bericht.Titel,
                NieuwsberichtId = bericht.NieuwsberichtId,
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Wijzigen(NieuwsberichtViewModel form)
        {
            if (ModelState.IsValid)
            {
                Nieuwsbericht nb = new Nieuwsbericht
                {
                    NieuwsberichtId = form.NieuwsberichtId,
                    Titel = form.Titel,
                    Inhoud = form.Inhoud,
                    Datum = form.Datum
                };
                nieuwsberichtService.Update(nb);
                return RedirectToAction("Index");
            }
            else
            {
                return View(form);
            }
        }

        [Route("Verwijderen/{id}")]
        public ActionResult VerwijderenForm(int id)
        {
            Nieuwsbericht nb = nieuwsberichtService.GetNieuwsberichtById(id);
            return View(nb);
        }

        [HttpPost]
        public ActionResult Verwijderen(int id)
        {
            nieuwsberichtService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}