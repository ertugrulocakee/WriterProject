namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "UserName", c => c.String());
            AlterColumn("dbo.Contacts", "UserMail", c => c.String());
            AlterColumn("dbo.Contacts", "Subject", c => c.String());
            AlterColumn("dbo.Contacts", "Message", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Message", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Contacts", "Subject", c => c.String(maxLength: 50));
            AlterColumn("dbo.Contacts", "UserMail", c => c.String(maxLength: 50));
            AlterColumn("dbo.Contacts", "UserName", c => c.String(maxLength: 50));
        }
    }
}
