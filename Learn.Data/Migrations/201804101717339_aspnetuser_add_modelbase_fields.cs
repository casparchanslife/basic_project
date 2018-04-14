namespace Learn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aspnetuser_add_modelbase_fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Status", c => c.String(maxLength: 1));
            AddColumn("dbo.AspNetUsers", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreatedBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CreatedBy_Id");
            AddForeignKey("dbo.AspNetUsers", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "CreatedBy_Id" });
            DropColumn("dbo.AspNetUsers", "CreatedBy_Id");
            DropColumn("dbo.AspNetUsers", "UpdateDate");
            DropColumn("dbo.AspNetUsers", "CreateDate");
            DropColumn("dbo.AspNetUsers", "Status");
        }
    }
}
