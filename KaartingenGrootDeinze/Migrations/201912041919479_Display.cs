namespace KaartingenGrootDeinze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Display : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaken", "Plaats", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaken", "Plaats", c => c.String(nullable: false));
        }
    }
}
