namespace Learn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fmk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.fmk_function",
                c => new
                    {
                        function_id = c.String(nullable: false, maxLength: 128),
                        function_group_id = c.Int(nullable: false),
                        url_area = c.String(),
                        url_controller = c.String(),
                        url_function = c.String(),
                        description = c.String(),
                        iorder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.function_id)
                .ForeignKey("dbo.fmk_function_group", t => t.function_group_id, cascadeDelete: true)
                .Index(t => t.function_group_id);
            
            CreateTable(
                "dbo.fmk_function_group",
                c => new
                    {
                        function_group_id = c.Int(nullable: false, identity: true),
                        iorder = c.Int(nullable: false),
                        locale = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.function_group_id);
            
            CreateTable(
                "dbo.fmk_function_role",
                c => new
                    {
                        function_id = c.String(nullable: false, maxLength: 128),
                        role_id = c.String(nullable: false, maxLength: 128),
                        can_update = c.Boolean(nullable: false),
                        can_insert = c.Boolean(nullable: false),
                        can_delete = c.Boolean(nullable: false),
                        can_read = c.Boolean(nullable: false),
                        can_copy = c.Boolean(nullable: false),
                        can_LoginList = c.Boolean(nullable: false),
                        can_ApplicationList = c.Boolean(nullable: false),
                        can_CinemaList = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.function_id, t.role_id })
                .ForeignKey("dbo.AspNetRoles", t => t.role_id, cascadeDelete: true)
                .ForeignKey("dbo.fmk_function", t => t.function_id, cascadeDelete: true)
                .Index(t => t.function_id)
                .Index(t => t.role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.fmk_function_role", "function_id", "dbo.fmk_function");
            DropForeignKey("dbo.fmk_function_role", "role_id", "dbo.AspNetRoles");
            DropForeignKey("dbo.fmk_function", "function_group_id", "dbo.fmk_function_group");
            DropIndex("dbo.fmk_function_role", new[] { "role_id" });
            DropIndex("dbo.fmk_function_role", new[] { "function_id" });
            DropIndex("dbo.fmk_function", new[] { "function_group_id" });
            DropTable("dbo.fmk_function_role");
            DropTable("dbo.fmk_function_group");
            DropTable("dbo.fmk_function");
        }
    }
}
