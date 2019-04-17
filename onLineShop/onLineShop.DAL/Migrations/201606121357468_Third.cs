namespace onLineShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropPrimaryKey("dbo.CartItems");
            AddColumn("dbo.CartItems", "ShoppingCartItemId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CartItems", "ShoppingCartItemId");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropPrimaryKey("dbo.CartItems");
            DropColumn("dbo.CartItems", "ShoppingCartItemId");
            AddPrimaryKey("dbo.CartItems", "ProductId");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Products", "ProductId");
        }
    }
}
