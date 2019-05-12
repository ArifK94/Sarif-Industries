namespace Sarif_Industries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUserTableV4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "JoinDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "JoinDate", c => c.DateTime(nullable: false));
        }
    }
}
