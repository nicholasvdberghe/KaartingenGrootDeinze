namespace KaartingenGrootDeinze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kaarting1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Kaartingen", "ZaakId");
            AddForeignKey("dbo.Kaartingen", "ZaakId", "dbo.Zaken", "ZaakId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kaartingen", "ZaakId", "dbo.Zaken");
            DropIndex("dbo.Kaartingen", new[] { "ZaakId" });
        }
    }
}
