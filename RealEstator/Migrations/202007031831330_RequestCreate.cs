namespace RealEstator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Issue = c.String(),
                    })
                .PrimaryKey(t => t.RequestID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Requests");
        }
    }
}
