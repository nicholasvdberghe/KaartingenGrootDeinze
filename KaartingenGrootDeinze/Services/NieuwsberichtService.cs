using KaartingenGrootDeinze.DAL;
using KaartingenGrootDeinze.Models;
using System.Collections.Generic;
using System.Linq;

namespace KaartingenGrootDeinze.Services
{
    public class NieuwsberichtService
    {
        public List<Nieuwsbericht> GetAlleNieuwsberichten()
        {
            using (var db = new KaartingContext())
            {
                return db.Nieuwsberichten.OrderByDescending(n => n.Datum).ToList();
            }
        }

        public Nieuwsbericht GetNieuwsberichtById(int id)
        {
            using (var db = new KaartingContext())
            {
                Nieuwsbericht nb = db.Nieuwsberichten.Find(id);
                return nb;
            }
        }

        public void Insert(Nieuwsbericht nb)
        {
            using (var db = new KaartingContext())
            {
                db.Nieuwsberichten.Add(nb);
                db.SaveChanges();
            }

        }

        public void Update(Nieuwsbericht nb)
        {
            using (var db = new KaartingContext())
            {
                var entity = db.Nieuwsberichten.Find(nb.NieuwsberichtId);
                if (entity != null)
                {
                    entity.Titel = nb.Titel;
                    entity.Inhoud = nb.Inhoud;
                    entity.Datum = nb.Datum;
                    db.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var db = new KaartingContext())
            {
                Nieuwsbericht nb = db.Nieuwsberichten.Find(id);
                db.Nieuwsberichten.Remove(nb);
                db.SaveChanges();
            }
        }
    }
}