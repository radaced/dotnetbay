namespace DotNetBay.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Location");
        }
    }
}
