namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixskill : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Skills", "BarOverall");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "BarOverall", c => c.Int(nullable: false));
        }
    }
}
