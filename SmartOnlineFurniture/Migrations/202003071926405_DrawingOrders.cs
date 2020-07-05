namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrawingOrders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DrawingOrders", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DrawingOrders", "Status", c => c.Boolean(nullable: false));
        }
    }
}
