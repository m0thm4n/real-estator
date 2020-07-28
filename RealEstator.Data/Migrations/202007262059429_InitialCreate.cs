namespace RealEstator.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Condo",
                c => new
                    {
                        CondoID = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Beds = c.Int(nullable: false),
                        Baths = c.Int(nullable: false),
                        SquareFootage = c.Int(nullable: false),
                        HasPool = c.Boolean(nullable: false),
                        IsWaterfront = c.Boolean(nullable: false),
                        Occupied = c.Boolean(nullable: false),
                        YearBuilt = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        RequestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CondoID)
                .ForeignKey("dbo.Request", t => t.RequestID, cascadeDelete: true)
                .Index(t => t.RequestID);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        RequestID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Issue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RequestID);
            
            CreateTable(
                "dbo.Home",
                c => new
                    {
                        HomeID = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Beds = c.Int(nullable: false),
                        Baths = c.Int(nullable: false),
                        SquareFootage = c.Int(nullable: false),
                        HasPool = c.Boolean(nullable: false),
                        IsWaterfront = c.Boolean(nullable: false),
                        Occupied = c.Boolean(nullable: false),
                        YearBuilt = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        RequestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HomeID)
                .ForeignKey("dbo.Request", t => t.RequestID, cascadeDelete: true)
                .Index(t => t.RequestID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Townhouse",
                c => new
                    {
                        TownhouseID = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Beds = c.Int(nullable: false),
                        Baths = c.Int(nullable: false),
                        SquareFootage = c.Int(nullable: false),
                        HasPool = c.Boolean(nullable: false),
                        IsWaterfront = c.Boolean(nullable: false),
                        Occupied = c.Boolean(nullable: false),
                        YearBuilt = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        RequestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TownhouseID)
                .ForeignKey("dbo.Request", t => t.RequestID, cascadeDelete: true)
                .Index(t => t.RequestID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Townhouse", "RequestID", "dbo.Request");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Home", "RequestID", "dbo.Request");
            DropForeignKey("dbo.Condo", "RequestID", "dbo.Request");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Townhouse", new[] { "RequestID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Home", new[] { "RequestID" });
            DropIndex("dbo.Condo", new[] { "RequestID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Townhouse");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Home");
            DropTable("dbo.Request");
            DropTable("dbo.Condo");
        }
    }
}
