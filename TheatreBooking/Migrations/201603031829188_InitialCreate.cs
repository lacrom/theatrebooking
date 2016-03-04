namespace TheatreBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booker",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Seat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RowName = c.String(),
                        RowNumber = c.String(),
                        SeatNumber = c.String(),
                        AreaDescription = c.String(),
                        Price = c.String(),
                        Information = c.String(),
                        Status = c.Int(nullable: false),
                        BookedAt = c.DateTime(),
                        SelectedAt = c.DateTime(),
                        BookedBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Booker", t => t.BookedBy_ID)
                .Index(t => t.BookedBy_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seat", "BookedBy_ID", "dbo.Booker");
            DropIndex("dbo.Seat", new[] { "BookedBy_ID" });
            DropTable("dbo.Seat");
            DropTable("dbo.Booker");
        }
    }
}
