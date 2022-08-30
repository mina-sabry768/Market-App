namespace SuperStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemTitle = c.String(),
                        ItemContent = c.String(),
                        ItemImage = c.String(),
                        Price = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropTable("dbo.Items");
        }
    }
}
