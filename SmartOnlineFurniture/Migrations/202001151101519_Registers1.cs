namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Registers1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Registers", "UserImage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Registers", "UserImage", c => c.String(nullable: false));
        }
    }
}
