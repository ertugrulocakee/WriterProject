namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtestimonial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        TestimonialID = c.Int(nullable: false, identity: true),
                        TestimonialValue = c.String(),
                        TestimonialStatus = c.Boolean(nullable: false),
                        WriterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestimonialID)
                .ForeignKey("dbo.Writers", t => t.WriterID, cascadeDelete: true)
                .Index(t => t.WriterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Testimonials", "WriterID", "dbo.Writers");
            DropIndex("dbo.Testimonials", new[] { "WriterID" });
            DropTable("dbo.Testimonials");
        }
    }
}
