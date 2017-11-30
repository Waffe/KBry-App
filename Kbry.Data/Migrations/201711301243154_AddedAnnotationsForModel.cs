namespace Kbry.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnnotationsForModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "Student_Id", "dbo.Students");
            DropIndex("dbo.Attendances", new[] { "Student_Id" });
            AlterColumn("dbo.Attendances", "Student_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "RegistrationCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Classes", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Attendances", "Student_Id");
            AddForeignKey("dbo.Attendances", "Student_Id", "dbo.Students", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Student_Id", "dbo.Students");
            DropIndex("dbo.Attendances", new[] { "Student_Id" });
            AlterColumn("dbo.Classes", "Name", c => c.String());
            AlterColumn("dbo.Students", "Email", c => c.String());
            AlterColumn("dbo.Students", "LastName", c => c.String());
            AlterColumn("dbo.Students", "FirstName", c => c.String());
            AlterColumn("dbo.Students", "RegistrationCode", c => c.String());
            AlterColumn("dbo.Attendances", "Student_Id", c => c.Int());
            CreateIndex("dbo.Attendances", "Student_Id");
            AddForeignKey("dbo.Attendances", "Student_Id", "dbo.Students", "Id");
        }
    }
}
