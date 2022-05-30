namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skilloverallfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "BarOverall", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skills", "BarOverall");
        }
    }
}
