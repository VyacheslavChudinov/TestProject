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
                        DownloaderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DownloaderId, cascadeDelete: false)
                .Index(t => t.DownloaderId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        DownloaderId = c.Int(nullable: false),
                        AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.DownloaderId, cascadeDelete: false)
                .Index(t => t.DownloaderId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.SongDownloadStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DownloadTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SongId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "UserId", "dbo.Users");
            DropForeignKey("dbo.Albums", "DownloaderId", "dbo.Users");
            DropForeignKey("dbo.SongDownloadStatistics", "SongId", "dbo.Songs");
            DropForeignKey("dbo.Songs", "DownloaderId", "dbo.Users");
            DropForeignKey("dbo.Songs", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Tokens", new[] { "UserId" });
            DropIndex("dbo.SongDownloadStatistics", new[] { "SongId" });
            DropIndex("dbo.Songs", new[] { "AlbumId" });
            DropIndex("dbo.Songs", new[] { "DownloaderId" });
            DropIndex("dbo.Albums", new[] { "DownloaderId" });
            DropTable("dbo.Tokens");
            DropTable("dbo.SongDownloadStatistics");
            DropTable("dbo.Songs");
            DropTable("dbo.Users");
            DropTable("dbo.Albums");
        }
    }
}
