namespace MiniMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateProducts : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [ArtUrl], [CategoryId]) VALUES (2, N'Gionee X1S', N'Cutting edge smartphone with 3gb RAM, 32 GB ROM, 13MP front and back camera', 15, N'\Images\GioneeX1s.jpg', 1)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [ArtUrl], [CategoryId]) VALUES (4, N'Tecno Pouvoir 4 Plus', N'Cutting edge smartphone with 4gb RAM, 64GB ROM, 18MP triple back and 13MP front camera', 24, N'\Images\TecnoPouvoir.jpg', 1)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [ArtUrl], [CategoryId]) VALUES (5, N'Hp ProBook 4540s', N'Incredibly fast corei3 PC built with a processor speed of 2.4ghz', 48, N'\Images\HpProbook.jpg', 2)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [ArtUrl], [CategoryId]) VALUES (6, N'MacBook', N'Modern, Fast, Sleek, the physical representation of all you dreamt of.', 100, N'\Images\macbook.jpg', 2)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [ArtUrl], [CategoryId]) VALUES (7, N'BlueTooth Speaker', N'High connectivity, designed to impress, long lasting battery', 5, N'\Images\speaker.jpg', 3)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [Price], [ArtUrl], [CategoryId]) VALUES (8, N'Gionee Charger', N'Fast charging with 2.1A usb cord. Original', 2, N'\Images\charger.jpg', 3)
SET IDENTITY_INSERT [dbo].[Products] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}
