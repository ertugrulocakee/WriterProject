namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messageclassadding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        SenderMail = c.String(),
                        ReceiverMail = c.String(),
                        Title = c.String(),
                        MessageDescription = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
