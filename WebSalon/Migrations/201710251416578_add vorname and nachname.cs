namespace WebSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvornameandnachname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FName", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "FName");
        }
    }
}
