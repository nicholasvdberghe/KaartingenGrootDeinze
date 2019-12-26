using KaartingenGrootDeinze.Models;
using KaartingenGrootDeinze.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;

namespace KaartingenGrootDeinze.Controllers
{
    [RoutePrefix("Kaartingen")]
    public class KaartingController : Controller
    {
        private KaartingService kaartingService = new KaartingService();
        private ZaakService zaakService = new ZaakService();
        private NieuwsberichtService nieuwsberichtService = new NieuwsberichtService();

        [Route]
        public ActionResult Index()
        {
            var ondergrens = DateTime.Today;
            List<Kaarting> kaartingen = kaartingService.GetGefilterdeKaartingen(ondergrens, null);
            return View(kaartingen);
        }

        [Route("Toevoegen")]
        public ActionResult ToevoegenForm()
        {
            KaartingViewModel viewModel = new KaartingViewModel();
            viewModel.Datum = DateTime.Now;
            viewModel.Startuur = DateTime.Now;
            viewModel.Zaken = zaakService.GetAlleZaken();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Toevoegen(KaartingViewModel form)
        {
            if (ModelState.IsValid)
            {
                Kaarting kaarting = new Kaarting();
                kaarting.Datum = form.Datum;
                kaarting.ZaakId = form.ZaakId;
                Zaak zaak = zaakService.GetZaakById(kaarting.ZaakId);
                if (zaak.Naam == "GEEN KAARTING")
                {
                    kaarting.Prijzengeld = 0;
                    kaarting.Startuur = new DateTime(kaarting.Datum.Year, kaarting.Datum.Month, kaarting.Datum.Day, 0, 0, 0);
                }
                else
                {
                    kaarting.Prijzengeld = form.Prijzengeld;
                    kaarting.Startuur = form.Startuur;
                }
                kaartingService.Insert(kaarting);
                return RedirectToAction("Index");
            }
            else
            {
                form.Zaken = zaakService.GetAlleZaken();
                return View(form);
            }
        }

        [Route("Wijzigen/{id}")]
        public ActionResult WijzigenForm(int id)
        {
            Kaarting kaarting = kaartingService.GetKaarting(id);
            KaartingViewModel viewModel = new KaartingViewModel();
            viewModel.KaartingId = kaarting.KaartingId;
            viewModel.Datum = kaarting.Datum;
            viewModel.Prijzengeld = kaarting.Prijzengeld;
            viewModel.ZaakId = kaarting.ZaakId;
            viewModel.Startuur = kaarting.Startuur;
            viewModel.Zaken = zaakService.GetAlleZaken();
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Wijzigen(KaartingViewModel form)
        {
            if (ModelState.IsValid)
            {
                var kaarting = new Kaarting();
                kaarting.KaartingId = form.KaartingId;
                kaarting.Datum = form.Datum;
                kaarting.ZaakId = form.ZaakId;
                Zaak zaak = zaakService.GetZaakById(form.ZaakId);
                kaarting.Zaak = zaak;
                if (zaak.Naam == "GEEN KAARTING")
                {
                    kaarting.Prijzengeld = 100;
                    kaarting.Startuur = new DateTime(kaarting.Datum.Year, kaarting.Datum.Month, kaarting.Datum.Day, 0, 0, 0);
                }
                else
                {
                    kaarting.Prijzengeld = form.Prijzengeld;
                    kaarting.Startuur = form.Startuur;
                }
                kaartingService.Update(kaarting);
                return RedirectToAction("Index");
            }
            return View(form);
        }

        [Route("Verwijderen/{id}")]
        public ActionResult VerwijderenForm(int id)
        {
            var kaarting = kaartingService.GetKaarting(id);
            kaarting.Zaak = kaartingService.GetZaakVanKaarting(id);
            return View(kaarting);
        }

        [HttpPost]
        public ActionResult Verwijderen(int id)
        {
            kaartingService.Delete(id);
            return RedirectToAction("Index");
        }

        [Route("PDF")]
        public FileResult ExportToPDF()
        {
            List<Kaarting> kaartingen = new List<Kaarting>();
            kaartingen = kaartingService.GetGefilterdeKaartingen(DateTime.Today, null);
            MemoryStream memStream = new MemoryStream();
            memStream = kaartingService.CreatePDF(memStream, kaartingen);

            byte[] bytesStream = memStream.ToArray();

            memStream = new MemoryStream();
            memStream.Write(bytesStream, 0, bytesStream.Length);
            memStream.Position = 0;

            return File(memStream, "application/pdf");
        }
    }
}