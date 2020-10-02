namespace DiscographyTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Album", "AlbumTitle", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Album", "AlbumTitle", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
