using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KaartingenGrootDeinze.Models;

namespace KaartingenGrootDeinze.DAL
{
    public class KaartingContext : DbContext
    {
        public KaartingContext() : base("DefaultConnection")
        {

        }

        public DbSet<Kaarting> Kaartingen { get; set; }
        public DbSet<Zaak> Zaken { get; set; }
        public DbSet<Nieuwsbericht> Nieuwsberichten { get; set; }

    }
}