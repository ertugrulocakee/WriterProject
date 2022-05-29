namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admintoproject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userName = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        role = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Admins");
        }
    }
}
