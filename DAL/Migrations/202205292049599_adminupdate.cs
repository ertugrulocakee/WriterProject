namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "userName", c => c.String());
            AlterColumn("dbo.Admins", "password", c => c.String());
            AlterColumn("dbo.Admins", "role", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "role", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.Admins", "password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Admins", "userName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
