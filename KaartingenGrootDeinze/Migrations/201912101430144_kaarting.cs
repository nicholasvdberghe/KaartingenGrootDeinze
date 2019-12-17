namespace KaartingenGrootDeinze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kaarting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kaartingen", "ZaakId", "dbo.Zaken");
            DropIndex("dbo.Kaartingen", new[] { "ZaakId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Kaartingen", "ZaakId");
            AddForeignKey("dbo.Kaartingen", "ZaakId", "dbo.Zaken", "ZaakId", cascadeDelete: true);
        }
    }
}
