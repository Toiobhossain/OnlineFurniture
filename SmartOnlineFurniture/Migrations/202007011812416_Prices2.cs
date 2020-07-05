namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prices2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prices", "PID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prices", "PID");
        }
    }
}
