namespace PassionProjUditesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderName = c.String(),
                        ArrivedFrom = c.String(),
                        DepartureTo = c.String(),
                        OrderDateTime = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        SectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Sectors", t => t.SectorID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.SectorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "SectorID", "dbo.Sectors");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "SectorID" });
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropTable("dbo.Orders");
        }
    }
}
