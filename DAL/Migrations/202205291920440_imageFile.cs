namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageFile : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.ImageFiles");
        }
    }
}
