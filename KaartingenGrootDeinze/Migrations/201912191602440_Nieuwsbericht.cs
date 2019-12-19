namespace KaartingenGrootDeinze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nieuwsbericht : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nieuwsberichten", "KaartingId", "dbo.Kaartingen");
            DropIndex("dbo.Nieuwsberichten", new[] { "KaartingId" });
            AlterColumn("dbo.Nieuwsberichten", "Titel", c => c.String(nullable: false));
            AlterColumn("dbo.Nieuwsberichten", "Inhoud", c => c.String(nullable: false));
            AlterColumn("dbo.Nieuwsberichten", "KaartingId", c => c.Int());
            CreateIndex("dbo.Nieuwsberichten", "KaartingId");
            AddForeignKey("dbo.Nieuwsberichten", "KaartingId", "dbo.Kaartingen", "KaartingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nieuwsberichten", "KaartingId", "dbo.Kaartingen");
            DropIndex("dbo.Nieuwsberichten", new[] { "KaartingId" });
            AlterColumn("dbo.Nieuwsberichten", "KaartingId", c => c.Int(nullable: false));
            AlterColumn("dbo.Nieuwsberichten", "Inhoud", c => c.String());
            AlterColumn("dbo.Nieuwsberichten", "Titel", c => c.String());
            CreateIndex("dbo.Nieuwsberichten", "KaartingId");
            AddForeignKey("dbo.Nieuwsberichten", "KaartingId", "dbo.Kaartingen", "KaartingId");
        }
    }
}
