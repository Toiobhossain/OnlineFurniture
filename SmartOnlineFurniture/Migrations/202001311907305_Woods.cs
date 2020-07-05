namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Woods : DbMigration
    {
        internal string WoodName;

        public override void Up()
        {
            CreateTable(
                "dbo.Woods",
                c => new
                    {
                        WoodId = c.Int(nullable: false, identity: true),
                        WoodName = c.String(),
                    })
                .PrimaryKey(t => t.WoodId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Woods");
        }
    }
}
