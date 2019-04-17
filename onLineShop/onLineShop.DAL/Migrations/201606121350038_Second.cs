namespace onLineShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CartItems", "ProductId");
            RenameColumn(table: "dbo.CartItems", name: "ShoppingCartItemId", newName: "ProductId");
            RenameIndex(table: "dbo.CartItems", name: "IX_ShoppingCartItemId", newName: "IX_ProductId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CartItems", name: "IX_ProductId", newName: "IX_ShoppingCartItemId");
            RenameColumn(table: "dbo.CartItems", name: "ProductId", newName: "ShoppingCartItemId");
            AddColumn("dbo.CartItems", "ProductId", c => c.Int(nullable: false));
        }
    }
}
