namespace SuperStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditItemModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "UserID");
            AddForeignKey("dbo.Items", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Items", new[] { "UserID" });
            DropColumn("dbo.Items", "UserID");
        }
    }
}
