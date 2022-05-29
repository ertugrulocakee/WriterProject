namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatetocontact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Date");
        }
    }
}
