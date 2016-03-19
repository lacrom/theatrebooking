namespace TheatreBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfaceandparty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booker", "Face", c => c.String());
            AddColumn("dbo.Booker", "Party", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booker", "Party");
            DropColumn("dbo.Booker", "Face");
        }
    }
}
