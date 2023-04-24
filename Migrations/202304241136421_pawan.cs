namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pawan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                        Image = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.Guid(nullable: false),
                        UpdatedBy = c.String(nullable: false),
                        UpdatedOn = c.Guid(nullable: false),
                        Status = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Credentials");
        }
    }
}
