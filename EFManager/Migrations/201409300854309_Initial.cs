namespace EFManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Album_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        ExpireDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "UserId", "dbo.Users");
            DropForeignKey("dbo.Songs", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Albums", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Songs", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Tokens", new[] { "UserId" });
            DropIndex("dbo.Songs", new[] { "User_Id" });
            DropIndex("dbo.Songs", new[] { "Album_Id" });
            DropIndex("dbo.Albums", new[] { "User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Tokens");
            DropTable("dbo.Songs");
            DropTable("dbo.Albums");
        }
    }
}
