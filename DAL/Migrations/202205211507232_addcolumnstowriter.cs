namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnstowriter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "WriterDescription", c => c.String());
            AddColumn("dbo.Writers", "WriterStatus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Writers", "WriterName", c => c.String());
            AlterColumn("dbo.Writers", "WriterSurName", c => c.String());
            AlterColumn("dbo.Writers", "WriterImage", c => c.String());
            AlterColumn("dbo.Writers", "WriterMail", c => c.String());
            AlterColumn("dbo.Writers", "WriterPassword", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "WriterPassword", c => c.String(maxLength: 20));
            AlterColumn("dbo.Writers", "WriterMail", c => c.String(maxLength: 50));
            AlterColumn("dbo.Writers", "WriterImage", c => c.String(maxLength: 100));
            AlterColumn("dbo.Writers", "WriterSurName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Writers", "WriterName", c => c.String(maxLength: 50));
            DropColumn("dbo.Writers", "WriterStatus");
            DropColumn("dbo.Writers", "WriterDescription");
        }
    }
}
