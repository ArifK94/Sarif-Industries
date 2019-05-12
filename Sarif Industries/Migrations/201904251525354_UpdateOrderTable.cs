namespace Sarif_Industries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "County", c => c.String());
            DropColumn("dbo.Orders", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Phone", c => c.String());
            DropColumn("dbo.Orders", "County");
        }
    }
}
