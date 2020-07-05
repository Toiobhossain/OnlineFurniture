namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrawingOrders1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DrawingOrders", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DrawingOrders", "Name");
        }
    }
}
