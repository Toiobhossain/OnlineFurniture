namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ProductID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ProductID");
        }
    }
}
