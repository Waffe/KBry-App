namespace Kbry.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatAndLongToAttendence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Attendances", "Latitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "Latitude");
            DropColumn("dbo.Attendances", "Longitude");
        }
    }
}
