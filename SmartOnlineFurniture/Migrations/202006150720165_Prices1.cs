namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prices1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prices", "ProductQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prices", "ProductQuantity");
        }
    }
}
