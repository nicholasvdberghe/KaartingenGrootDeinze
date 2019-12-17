namespace KaartingenGrootDeinze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zaak : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaken", "Naam", c => c.String(nullable: false));
            AlterColumn("dbo.Zaken", "Plaats", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaken", "Plaats", c => c.String());
            AlterColumn("dbo.Zaken", "Naam", c => c.String());
        }
    }
}
