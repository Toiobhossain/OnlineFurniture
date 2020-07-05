namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Woods1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Woods", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Woods", "Price");
        }
    }
}
