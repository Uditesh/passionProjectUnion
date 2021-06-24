namespace PassionProjUditesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeMobNum = c.String(),
                        EmployeeEmail = c.String(),
                        EmployeeAdrress = c.String(),
                        SectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Sectors", t => t.SectorID, cascadeDelete: true)
                .Index(t => t.SectorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "SectorID", "dbo.Sectors");
            DropIndex("dbo.Employees", new[] { "SectorID" });
            DropTable("dbo.Employees");
        }
    }
}
