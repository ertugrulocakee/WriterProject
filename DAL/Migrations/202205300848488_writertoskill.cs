namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class writertoskill : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "Writer_WriterID", c => c.Int());
            CreateIndex("dbo.Skills", "Writer_WriterID");
            AddForeignKey("dbo.Skills", "Writer_WriterID", "dbo.Writers", "WriterID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "Writer_WriterID", "dbo.Writers");
            DropIndex("dbo.Skills", new[] { "Writer_WriterID" });
            DropColumn("dbo.Skills", "Writer_WriterID");
        }
    }
}
