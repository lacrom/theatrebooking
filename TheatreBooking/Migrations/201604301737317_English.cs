namespace TheatreBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class English : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seat", "RowNameEn", c => c.String());
            AddColumn("dbo.Seat", "AreaDescriptionEn", c => c.String());
            AddColumn("dbo.Seat", "InformationEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Seat", "InformationEn");
            DropColumn("dbo.Seat", "AreaDescriptionEn");
            DropColumn("dbo.Seat", "RowNameEn");
        }
    }
}
