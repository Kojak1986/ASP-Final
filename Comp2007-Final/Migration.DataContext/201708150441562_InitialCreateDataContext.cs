namespace Comp2007_Final.Migration.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateDataContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colours",
                c => new
                    {
                        ColourId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        Name = c.String(nullable: false, maxLength: 250),
                    CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                })
                .PrimaryKey(t => t.ColourId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        ItemId = c.String(nullable: false, maxLength: 128),
                        ColourId = c.String(nullable: false, maxLength: 128),
                        FinishId = c.String(nullable: false, maxLength: 128),
                    CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.ItemFinish", t => t.FinishId)
                .ForeignKey("dbo.Colours", t => t.ColourId)
                .Index(t => t.ItemId)
                .Index(t => t.ColourId)
                .Index(t => t.FinishId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        Name = c.String(nullable: false, maxLength: 250),
                    CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.ItemFinish",
                c => new
                    {
                        FinishId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.FinishId);
            
          
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ColourId", "dbo.Colours");
            DropForeignKey("dbo.Orders", "FinishId", "dbo.ItemFinish");
            DropForeignKey("dbo.Orders", "ItemId", "dbo.Items");
            DropIndex("dbo.Orders", new[] { "FinishId" });
            DropIndex("dbo.Orders", new[] { "ColourId" });
            DropIndex("dbo.Orders", new[] { "ItemId" });
            DropTable("dbo.ItemFinish");
            DropTable("dbo.Items");
            DropTable("dbo.Orders");
            DropTable("dbo.Colours");
        }
    }
}
