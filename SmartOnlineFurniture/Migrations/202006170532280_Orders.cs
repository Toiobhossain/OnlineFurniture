namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        orderId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        ProductPrice = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                        PaymentMethod = c.String(),
                    })
                .PrimaryKey(t => t.orderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
