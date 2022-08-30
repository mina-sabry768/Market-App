namespace SuperStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        BuyDate = c.DateTime(nullable: false),
                        ItemId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ItemId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BuyItems", "ItemId", "dbo.Items");
            DropIndex("dbo.BuyItems", new[] { "UserId" });
            DropIndex("dbo.BuyItems", new[] { "ItemId" });
            DropTable("dbo.BuyItems");
        }
    }
}
