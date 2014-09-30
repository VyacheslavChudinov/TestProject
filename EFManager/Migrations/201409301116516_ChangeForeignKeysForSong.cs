namespace EFManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeForeignKeysForSong : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Songs", name: "Album_Id", newName: "AlbumId");
            RenameColumn(table: "dbo.Songs", name: "Downloader_Id", newName: "DownloaderId");
            RenameIndex(table: "dbo.Songs", name: "IX_Downloader_Id", newName: "IX_DownloaderId");
            RenameIndex(table: "dbo.Songs", name: "IX_Album_Id", newName: "IX_AlbumId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Songs", name: "IX_AlbumId", newName: "IX_Album_Id");
            RenameIndex(table: "dbo.Songs", name: "IX_DownloaderId", newName: "IX_Downloader_Id");
            RenameColumn(table: "dbo.Songs", name: "DownloaderId", newName: "Downloader_Id");
            RenameColumn(table: "dbo.Songs", name: "AlbumId", newName: "Album_Id");
        }
    }
}
