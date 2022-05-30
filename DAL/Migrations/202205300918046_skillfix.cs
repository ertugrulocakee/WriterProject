namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skillfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "Writer_WriterID", "dbo.Writers");
            DropIndex("dbo.Skills", new[] { "Writer_WriterID" });
            RenameColumn(table: "dbo.Skills", name: "Writer_WriterID", newName: "WriterID");
            AddColumn("dbo.Skills", "SkillStatus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Skills", "WriterID", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "WriterID");
            AddForeignKey("dbo.Skills", "WriterID", "dbo.Writers", "WriterID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "WriterID", "dbo.Writers");
            DropIndex("dbo.Skills", new[] { "WriterID" });
            AlterColumn("dbo.Skills", "WriterID", c => c.Int());
            DropColumn("dbo.Skills", "SkillStatus");
            RenameColumn(table: "dbo.Skills", name: "WriterID", newName: "Writer_WriterID");
            CreateIndex("dbo.Skills", "Writer_WriterID");
            AddForeignKey("dbo.Skills", "Writer_WriterID", "dbo.Writers", "WriterID");
        }
    }
}
