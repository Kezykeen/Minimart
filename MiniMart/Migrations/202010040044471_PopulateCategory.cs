namespace MiniMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategory : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Mobile Phones')
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Laptops')
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Accessories')
SET IDENTITY_INSERT [dbo].[Categories] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}
