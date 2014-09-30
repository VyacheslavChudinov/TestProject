namespace EFManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSongDownloadStatistics : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "User_Id", "dbo.Users");
            DropIndex("dbo.Songs", new[] { "User_Id" });
            RenameColumn(table: "dbo.Albums", name: "User_Id", newName: "Downloader_Id");
            RenameColumn(table: "dbo.Songs", name: "User_Id", newName: "Downloader_Id");
            RenameIndex(table: "dbo.Albums", name: "IX_User_Id", newName: "IX_Downloader_Id");
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
            
            AlterColumn("dbo.Songs", "Downloader_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Songs", "Downloader_Id");
            AddForeignKey("dbo.Songs", "Downloader_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "Downloader_Id", "dbo.Users");
            DropForeignKey("dbo.SongDownloadStatistics", "SongId", "dbo.Songs");
            DropIndex("dbo.SongDownloadStatistics", new[] { "SongId" });
            DropIndex("dbo.Songs", new[] { "Downloader_Id" });
            AlterColumn("dbo.Songs", "Downloader_Id", c => c.Int());
            DropTable("dbo.SongDownloadStatistics");
            RenameIndex(table: "dbo.Albums", name: "IX_Downloader_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Songs", name: "Downloader_Id", newName: "User_Id");
            RenameColumn(table: "dbo.Albums", name: "Downloader_Id", newName: "User_Id");
            CreateIndex("dbo.Songs", "User_Id");
            AddForeignKey("dbo.Songs", "User_Id", "dbo.Users", "Id");
        }
    }
}
