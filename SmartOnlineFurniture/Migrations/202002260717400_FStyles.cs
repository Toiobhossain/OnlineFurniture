namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FStyles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FStyles",
                c => new
                    {
                        StyleId = c.Int(nullable: false, identity: true),
                        StyleName = c.String(),
                    })
                .PrimaryKey(t => t.StyleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FStyles");
        }
    }
}
