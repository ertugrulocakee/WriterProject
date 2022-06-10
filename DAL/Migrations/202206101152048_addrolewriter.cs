namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrolewriter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Writers", "role");
        }
    }
}
