namespace Sarif_Industries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Country", c => c.String());
            AddColumn("dbo.Orders", "PostCode", c => c.String());
            DropColumn("dbo.Users", "IsAdmin");
            DropColumn("dbo.Orders", "PostalCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PostalCode", c => c.String());
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "PostCode");
            DropColumn("dbo.Users", "Country");
        }
    }
}
