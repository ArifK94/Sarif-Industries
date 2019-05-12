namespace Sarif_Industries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Thumbnail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Thumbnail", c => c.String());
        }
    }
}
