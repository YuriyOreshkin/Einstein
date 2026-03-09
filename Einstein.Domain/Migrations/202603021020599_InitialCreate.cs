namespace Einstein.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EVENTs",
                c => new
                    {
                        EventID = c.Long(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        IsAllDay = c.Boolean(nullable: false),
                        RecurrenceRule = c.String(),
                        RecurrenceID = c.Int(),
                        RecurrenceException = c.String(),
                        StartTimezone = c.String(),
                        EndTimezone = c.String(),
                        MaxPersons = c.Int(nullable: false),
                        MaxPersons14 = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.ORDERs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DATE = c.DateTime(nullable: false),
                        EVENTID = c.Long(nullable: false),
                        PERSONS = c.Short(nullable: false),
                        PERSONS14 = c.Short(nullable: false),
                        PHONE = c.String(),
                        EMAIL = c.String(),
                        INFORM = c.Boolean(nullable: false),
                        DESCRIPTION = c.String(),
                        PREPAY = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EVENTs", t => t.EVENTID, cascadeDelete: true)
                .Index(t => t.EVENTID);
            
            CreateTable(
                "dbo.PAYMENTs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DATE = c.DateTime(nullable: false),
                        TERMINALID = c.String(),
                        ORDERID = c.Long(nullable: false),
                        SUCCESS = c.Boolean(nullable: false),
                        STATUS = c.String(),
                        PAYMENTID = c.String(),
                        ERRORCODE = c.Int(nullable: false),
                        AMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CARDID = c.Long(nullable: false),
                        PAN = c.String(),
                        EXPDATE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ROLEs",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        NAME = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.USERs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LOGIN = c.String(),
                        PASSWORD = c.String(),
                        ROLEID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ROLEs", t => t.ROLEID, cascadeDelete: true)
                .Index(t => t.ROLEID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.USERs", "ROLEID", "dbo.ROLEs");
            DropForeignKey("dbo.ORDERs", "EVENTID", "dbo.EVENTs");
            DropIndex("dbo.USERs", new[] { "ROLEID" });
            DropIndex("dbo.ORDERs", new[] { "EVENTID" });
            DropTable("dbo.USERs");
            DropTable("dbo.ROLEs");
            DropTable("dbo.PAYMENTs");
            DropTable("dbo.ORDERs");
            DropTable("dbo.EVENTs");
        }
    }
}
