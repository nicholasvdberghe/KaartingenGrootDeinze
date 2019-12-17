using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KaartingenGrootDeinze.Models;
using KaartingenGrootDeinze.DAL;
using System.Data.Entity;

namespace KaartingenGrootDeinze.Services
{
    public class KaartingService
    {
        public List<Kaarting> GetToekomstigeKaartingen (DateTime laatsteDatum)
        {
            using (var db = new KaartingContext())
            {
                List<Kaarting> kaartingen = new List<Kaarting>();
                kaartingen = db.Kaartingen.Include("Zaak")
                    .Where(k => k.Datum <= laatsteDatum)
                    .OrderBy(k => k.Datum)
                    .ThenBy(k => k.Startuur)
                    .ToList();
                return kaartingen;
            }
        }

        public Kaarting GetKaarting(int id)
        {
            using (var db = new KaartingContext())
            {
                Kaarting kaarting = db.Kaartingen.Find(id);
                return kaarting;
            }
        }

        public Zaak GetZaakVanKaarting(int? id)
        {
            using (var db = new KaartingContext())
            {
                Zaak zaak = db.Zaken.Find(id);
                return zaak;
            }
        }

        public void Insert(Kaarting kaarting)
        {
            using (var db = new KaartingContext())
            {
                db.Kaartingen.Add(kaarting);
                db.SaveChanges();
            }
        }

        public void Update(Kaarting kaarting)
        {
            using (var db = new KaartingContext())
            {
                db.Kaartingen.Attach(kaarting);
                db.Entry(kaarting).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new KaartingContext())
            {
                Kaarting kaarting = db.Kaartingen.Find(id);
                db.Kaartingen.Remove(kaarting);
                db.SaveChanges();
            }
        }

    }
}