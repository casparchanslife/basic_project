namespace Learn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aspnetuser_add_modelbase_fields2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "CreatedById" });
            DropIndex("dbo.AspNetUsers", new[] { "UpdatedById" });
            AlterColumn("dbo.AspNetUsers", "CreatedById", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "UpdatedById", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CreatedById");
            CreateIndex("dbo.AspNetUsers", "UpdatedById");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "UpdatedById" });
            DropIndex("dbo.AspNetUsers", new[] { "CreatedById" });
            AlterColumn("dbo.AspNetUsers", "UpdatedById", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "CreatedById", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "UpdatedById");
            CreateIndex("dbo.AspNetUsers", "CreatedById");
        }
    }
}
