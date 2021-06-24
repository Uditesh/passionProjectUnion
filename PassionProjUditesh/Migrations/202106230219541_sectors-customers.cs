namespace PassionProjUditesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sectorscustomers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerMobNum = c.String(),
                        CustomerEmail = c.String(),
                        CustomerAddress = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Sectors",
                c => new
                    {
                        SectorID = c.Int(nullable: false, identity: true),
                        SectorName = c.String(),
                    })
                .PrimaryKey(t => t.SectorID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sectors");
            DropTable("dbo.Customers");
        }
    }
}
