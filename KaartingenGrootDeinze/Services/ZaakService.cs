using KaartingenGrootDeinze.DAL;
using KaartingenGrootDeinze.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KaartingenGrootDeinze.Services
{
    public class ZaakService
    {
        public List<Zaak> GetAlleZaken()
        {
            using (var db = new KaartingContext())
            {
                List<Zaak> zaken = new List<Zaak>();
                foreach (var zaak in db.Zaken.OrderBy(z => z.Naam))
                {
                        zaken.Add(zaak);
                }
                return zaken;
            }
        }

        public Zaak GetZaakById(int id)
        {
            using (var db = new KaartingContext())
            {
                return (db.Zaken.Find(id));
            }
        }

        public List<Kaarting> GetKaartingenFromZaak(int id)
        {
            using (var db = new KaartingContext())
            {
                return db.Kaartingen.Where(m => m.ZaakId == id).ToList();
            }
        }

        public void Insert(Zaak zaak)
        {
            using (var db = new KaartingContext())
            {
                db.Zaken.Add(zaak);
                db.SaveChanges();
            }
        }

        public void Update(Zaak nieuweZaak)
        {
            using (var db = new KaartingContext())
            {
                db.Zaken.Attach(nieuweZaak);
                db.Entry(nieuweZaak).State = EntityState.Modified;
                db.SaveChanges();
                /* ALTERNATIEF
                 =============
                Zaak oudeZaak = db.Zaken.Find(nieuweZaak.ZaakId);
                oudeZaak.Naam = nieuweZaak.Naam;
                oudeZaak.Plaats = nieuweZaak.Plaats;
                db.SaveChanges();
                */
            }
        }

        public void Delete(int id)
        {
            using (var db = new KaartingContext())
            {
                Zaak zaak = db.Zaken.Find(id);
                db.Zaken.Remove(zaak);
                db.SaveChanges();
            }
        }
    }
}