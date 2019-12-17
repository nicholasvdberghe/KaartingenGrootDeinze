namespace KaartingenGrootDeinze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kaartingen",
                c => new
                    {
                        KaartingId = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        Startuur = c.DateTime(nullable: false),
                        Prijzengeld = c.Int(nullable: false),
                        ZaakId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KaartingId)
                .ForeignKey("dbo.Zaken", t => t.ZaakId, cascadeDelete: true)
                .Index(t => t.ZaakId);
            
            CreateTable(
                "dbo.Nieuwsberichten",
                c => new
                    {
                        NieuwsberichtId = c.Int(nullable: false, identity: true),
                        Titel = c.String(),
                        Inhoud = c.String(),
                        Datum = c.DateTime(nullable: false),
                        KaartingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NieuwsberichtId)
                .ForeignKey("dbo.Kaartingen", t => t.KaartingId, cascadeDelete: true)
                .Index(t => t.KaartingId);
            
            CreateTable(
                "dbo.Zaken",
                c => new
                    {
                        ZaakId = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Plaats = c.String(),
                    })
                .PrimaryKey(t => t.ZaakId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kaartingen", "ZaakId", "dbo.Zaken");
            DropForeignKey("dbo.Nieuwsberichten", "KaartingId", "dbo.Kaartingen");
            DropIndex("dbo.Nieuwsberichten", new[] { "KaartingId" });
            DropIndex("dbo.Kaartingen", new[] { "ZaakId" });
            DropTable("dbo.Zaken");
            DropTable("dbo.Nieuwsberichten");
            DropTable("dbo.Kaartingen");
        }
    }
}
