namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeimagefile : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ImageFiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        imageID = c.Int(nullable: false, identity: true),
                        imageName = c.String(),
                        imagePath = c.String(),
                    })
                .PrimaryKey(t => t.imageID);
            
        }
    }
}
