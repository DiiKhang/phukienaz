namespace PhuKienAZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 32),
                        ControllerId = c.String(maxLength: 2),
                        Action = c.String(),
                        RecordId = c.String(maxLength: 32),
                        Datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MyControllers", t => t.ControllerId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ControllerId);
            
            CreateTable(
                "dbo.MyControllers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 2),
                        EngName = c.String(maxLength: 64),
                        VieName = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 64),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 128),
                        Male = c.Boolean(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Picture = c.String(nullable: false, maxLength: 64),
                        Address = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 32),
                        Manager = c.Boolean(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true, name: "IX_USERNAME");
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(maxLength: 128),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        ManufacturerId = c.Int(nullable: false),
                        ProducingCountry = c.String(nullable: false, maxLength: 64),
                        WarrantyMonths = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        SellOff = c.Int(),
                        Unit = c.String(nullable: false, maxLength: 32),
                        Picture = c.String(maxLength: 128),
                        Description = c.String(storeType: "ntext"),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(maxLength: 128),
                        CustomerId = c.String(maxLength: 32),
                        Content = c.String(storeType: "ntext"),
                        Datetime = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 64),
                        Phone = c.String(nullable: false),
                        BankAccountCode = c.String(maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 128),
                        Address = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 32),
                        Male = c.Boolean(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 32),
                        Destination = c.String(maxLength: 128),
                        Description = c.String(maxLength: 64),
                        PayByBank = c.Boolean(nullable: false),
                        Date = c.DateTime(),
                        Completed = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Picture = c.String(maxLength: 128),
                        Brief = c.String(nullable: false, maxLength: 512),
                        Content = c.String(storeType: "ntext"),
                        UserId = c.String(maxLength: 32),
                        Date = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Likes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Likes", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Comments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Activities", "UserId", "dbo.Users");
            DropForeignKey("dbo.Activities", "ControllerId", "dbo.MyControllers");
            DropIndex("dbo.News", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Likes", new[] { "CustomerId" });
            DropIndex("dbo.Likes", new[] { "ProductId" });
            DropIndex("dbo.Comments", new[] { "CustomerId" });
            DropIndex("dbo.Comments", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Users", "IX_USERNAME");
            DropIndex("dbo.Activities", new[] { "ControllerId" });
            DropIndex("dbo.Activities", new[] { "UserId" });
            DropTable("dbo.News");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Likes");
            DropTable("dbo.Customers");
            DropTable("dbo.Comments");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Users");
            DropTable("dbo.MyControllers");
            DropTable("dbo.Activities");
        }
    }
}
