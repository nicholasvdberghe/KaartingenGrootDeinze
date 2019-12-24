namespace KaartingenGrootDeinze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nieuwsbericht2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Nieuwsberichten", "Titel", c => c.String());
            AlterColumn("dbo.Nieuwsberichten", "Inhoud", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nieuwsberichten", "Inhoud", c => c.String(nullable: false));
            AlterColumn("dbo.Nieuwsberichten", "Titel", c => c.String(nullable: false));
        }
    }
}
