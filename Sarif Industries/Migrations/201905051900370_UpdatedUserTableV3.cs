namespace Sarif_Industries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUserTableV3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "JoinDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "JoinDate");
        }
    }
}
