﻿using KaartingenGrootDeinze.DAL;
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
                return db.Nieuwsberichten.Include("Kaarting").OrderByDescending(n => n.Datum).ToList();
            }
        }

        public Nieuwsbericht GetNieuwsberichtById(int id)
        {
            using (var db = new KaartingContext())
            {
                Nieuwsbericht nb = db.Nieuwsberichten.Find(id);
                nb.Kaarting = db.Kaartingen.Find(nb.KaartingId);
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
                    entity.KaartingId = nb.KaartingId;
                    entity.Datum = nb.Datum;
                    db.SaveChanges();
                }
            }
        }
    }
}