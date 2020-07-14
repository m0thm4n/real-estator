namespace RealEstator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Homes", "CondoID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Homes", "CondoID");
        }
    }
}
