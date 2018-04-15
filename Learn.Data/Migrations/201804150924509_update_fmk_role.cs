namespace Learn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_fmk_role : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.fmk_function_role", "can_LoginList");
            DropColumn("dbo.fmk_function_role", "can_ApplicationList");
            DropColumn("dbo.fmk_function_role", "can_CinemaList");
        }
        
        public override void Down()
        {
            AddColumn("dbo.fmk_function_role", "can_CinemaList", c => c.Boolean(nullable: false));
            AddColumn("dbo.fmk_function_role", "can_ApplicationList", c => c.Boolean(nullable: false));
            AddColumn("dbo.fmk_function_role", "can_LoginList", c => c.Boolean(nullable: false));
        }
    }
}
