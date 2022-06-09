namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entitiesupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "email", c => c.String());
            DropTable("dbo.Abouts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        AboutID = c.Int(nullable: false, identity: true),
                        AboutDetails1 = c.String(maxLength: 1000),
                        AboutDetails2 = c.String(maxLength: 1000),
                        AboutImage1 = c.String(maxLength: 100),
                        AboutImage2 = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.AboutID);
            
            DropColumn("dbo.Admins", "email");
        }
    }
}
