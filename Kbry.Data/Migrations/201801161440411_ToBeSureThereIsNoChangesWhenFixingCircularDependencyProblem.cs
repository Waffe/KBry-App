namespace Kbry.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToBeSureThereIsNoChangesWhenFixingCircularDependencyProblem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Classes", newName: "SchoolClasses");
            DropForeignKey("dbo.Attendances", "Student_Id", "dbo.Students");
            DropIndex("dbo.Attendances", new[] { "Student_Id" });
            RenameColumn(table: "dbo.Students", name: "Class_Id", newName: "SchoolClass_Id");
            RenameIndex(table: "dbo.Students", name: "IX_Class_Id", newName: "IX_SchoolClass_Id");
            AlterColumn("dbo.Attendances", "Student_Id", c => c.Int());
            CreateIndex("dbo.Attendances", "Student_Id");
            AddForeignKey("dbo.Attendances", "Student_Id", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Student_Id", "dbo.Students");
            DropIndex("dbo.Attendances", new[] { "Student_Id" });
            AlterColumn("dbo.Attendances", "Student_Id", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Students", name: "IX_SchoolClass_Id", newName: "IX_Class_Id");
            RenameColumn(table: "dbo.Students", name: "SchoolClass_Id", newName: "Class_Id");
            CreateIndex("dbo.Attendances", "Student_Id");
            AddForeignKey("dbo.Attendances", "Student_Id", "dbo.Students", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.SchoolClasses", newName: "Classes");
        }
    }
}
