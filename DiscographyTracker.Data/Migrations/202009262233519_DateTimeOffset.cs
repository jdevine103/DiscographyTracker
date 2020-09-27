namespace DiscographyTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeOffset : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Album", "ReleaseDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Album", "ReleaseDate", c => c.DateTime(nullable: false));
        }
    }
}
