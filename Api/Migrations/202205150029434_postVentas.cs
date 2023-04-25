namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postVentas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ventas", "Total", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ventas", "Total", c => c.Int(nullable: false));
        }
    }
}
