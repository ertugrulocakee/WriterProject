namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusaddtomessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "ContactStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "MessageStatus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Headings", "HeadingName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Headings", "HeadingName", c => c.String(maxLength: 50));
            DropColumn("dbo.Messages", "MessageStatus");
            DropColumn("dbo.Contacts", "ContactStatus");
        }
    }
}
