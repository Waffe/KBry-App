namespace Kbry.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiKeyAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApiKeys");
        }
    }
}