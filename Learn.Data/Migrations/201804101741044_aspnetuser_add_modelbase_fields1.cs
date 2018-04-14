namespace Learn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aspnetuser_add_modelbase_fields1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "CreatedBy_Id" });
            RenameColumn(table: "dbo.AspNetUsers", name: "CreatedBy_Id", newName: "CreatedById");
            AddColumn("dbo.AspNetUsers", "UpdatedById", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "CreatedById", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CreatedById");
            CreateIndex("dbo.AspNetUsers", "UpdatedById");
            AddForeignKey("dbo.AspNetUsers", "UpdatedById", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UpdatedById", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "UpdatedById" });
            DropIndex("dbo.AspNetUsers", new[] { "CreatedById" });
            AlterColumn("dbo.AspNetUsers", "CreatedById", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "UpdatedById");
            RenameColumn(table: "dbo.AspNetUsers", name: "CreatedById", newName: "CreatedBy_Id");
            CreateIndex("dbo.AspNetUsers", "CreatedBy_Id");
        }
    }
}
