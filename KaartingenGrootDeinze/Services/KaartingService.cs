using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using KaartingenGrootDeinze.DAL;
using KaartingenGrootDeinze.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace KaartingenGrootDeinze.Services
{
    public class KaartingService
    {
        public List<Kaarting> GetGefilterdeKaartingen(DateTime ondergrens, DateTime? bovengrens)
        {
            using (var db = new KaartingContext())
            {
                //er is een ookb ovengrens
                if (bovengrens != null)
                {
                    List<Kaarting> kaartingen = new List<Kaarting>();
                    kaartingen = db.Kaartingen.Include("Zaak")
                        .Where(k => (k.Datum >= ondergrens) && (k.Datum <= bovengrens))
                        .OrderBy(k => k.Datum)
                        .ThenBy(k => k.Startuur)
                        .ToList();
                    return kaartingen;
                }
                else
                {
                    List<Kaarting> kaartingen = new List<Kaarting>();
                    kaartingen = db.Kaartingen.Include("Zaak")
                        .Where(k => k.Datum >= ondergrens)
                        .OrderBy(k => k.Datum)
                        .ThenBy(k => k.Startuur)
                        .ToList();
                    return kaartingen;
                }
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

        public MemoryStream CreatePDF(MemoryStream memStream, string[,] kaartingen)
        {
            PdfWriter writer = new PdfWriter(memStream);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document doc = new Document(pdfDoc, new iText.Kernel.Geom.PageSize(612, 792));
            doc.SetMargins(40f, 40f, 40f, 40f);

            //kaartingkolommen: Datum, Zaak.Naam, Zaak.Plaats, Prijzengeld, Startuur

            Table table = new Table(5);
            for(int rijnr = 0; rijnr < (kaartingen.Length/5); rijnr++)
            {

                    Cell cellDatum = new Cell(1, 1).Add(new Paragraph(kaartingen[rijnr, 0].ToString())).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT).SetPaddingRight(20f);
                    table.AddCell(cellDatum);
                    if (kaartingen[rijnr, 1].ToString() != "GEEN KAARTING")
                    {
                        Cell cellZaakNaam = new Cell(1, 1).Add(new Paragraph(kaartingen[rijnr, 1].ToString())).SetBorder(Border.NO_BORDER).SetPaddingRight(25f);
                        table.AddCell(cellZaakNaam);
                        Cell cellZaakPlaats = new Cell(1, 1).Add(new Paragraph(kaartingen[rijnr,2].ToString())).SetBorder(Border.NO_BORDER).SetPaddingRight(40f);
                        table.AddCell(cellZaakPlaats);

                        Cell cellPrijzengeld = new Cell(1, 1).Add(new Paragraph(kaartingen[rijnr, 3].ToString())).SetBorder(Border.NO_BORDER).SetPaddingRight(20f);
                        table.AddCell(cellPrijzengeld);

                        Cell cellStartuur = new Cell(1, 1).Add(new Paragraph(kaartingen[rijnr, 4].ToString())).SetBorder(Border.NO_BORDER);
                        table.AddCell(cellStartuur);
                    }
                    else
                    {
                        Cell cell = new Cell(1, 4).Add(new Paragraph(kaartingen[rijnr, 1].ToString())).SetBorder(Border.NO_BORDER);
                        table.AddCell(cell);
                    }
            }

            /*
             * //ging niet meer omdat cultureinfo ook toegepast moest worden op data in pdf
            int kolomnr = 0;
            foreach (string kaarting in kaartingen)
            {
                Cell cellDatum = new Cell(1, 1).Add(new Paragraph(kaarting[0].ToString())).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT).SetPaddingRight(20f);
                table.AddCell(cellDatum);
                if (kaarting[1].ToString() != "GEEN KAARTING")
                {
                    Cell cellZaakNaam = new Cell(1, 1).Add(new Paragraph(kaarting[1].ToString())).SetBorder(Border.NO_BORDER).SetPaddingRight(25f);
                    table.AddCell(cellZaakNaam);
                    Cell cellZaakPlaats = new Cell(1, 1).Add(new Paragraph(kaarting[2].ToString())).SetBorder(Border.NO_BORDER).SetPaddingRight(40f);
                    table.AddCell(cellZaakPlaats);

                    Cell cellPrijzengeld = new Cell(1, 1).Add(new Paragraph(kaarting[3].ToString())).SetBorder(Border.NO_BORDER).SetPaddingRight(20f);
                    table.AddCell(cellPrijzengeld);

                    Cell cellStartuur = new Cell(1, 1).Add(new Paragraph(kaarting[4].ToString())).SetBorder(Border.NO_BORDER);
                    table.AddCell(cellStartuur);
                    kolomnr++;
                }
                else
                {
                    Cell cell = new Cell(1, 4).Add(new Paragraph(kaarting[1].ToString())).SetBorder(Border.NO_BORDER);
                    table.AddCell(cell);
                    kolomnr++;
                }
            }
            */
            try
            {
                doc.Add(new Paragraph("KAARTINGEN BIEDEN 2019 - DEINZE EN OMSTREKEN").SetBold());
                doc.Add(table);
                doc.Add(new Paragraph("Inschrijvingen & inlichtingen bij Gaby (09/386.56.32 - 0474/37.14.09) en Roger").SetBold());
                doc.Add(new Paragraph("Info: http://kaartingendeinze.inksniper.be").SetBold());
                doc.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return memStream;
        }

    }
}