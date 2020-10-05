namespace MiniMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2275ad98-676b-4633-9ff4-8d63a7338739', N'admin@minimart.com', 0, N'APYSH2EJC03YV8y0iQ3ZD2NDP2m269CYNQe2Ge58n+MDxGdlq/AcexF5Ai8BAfp6uw==', N'783ce79c-3f7e-4db7-a727-fa220fafa743', NULL, 0, 0, NULL, 1, 0, N'admin@minimart.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a0b50aaf-f130-4340-a517-3fbeb34eee12', N'guest@minimart.com', 0, N'AGzbpvhwkAZyD7dokV7kf5Jc8Oex5qVbuaF3hpJzaSIRneks9yLWdmzlcvYbHuq2ag==', N'd3f218eb-827e-449e-bdb6-bdc771cce01d', NULL, 0, 0, NULL, 1, 0, N'guest@minimart.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'44af6ccc-fc4f-4286-be24-8cc4c2efd567', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2275ad98-676b-4633-9ff4-8d63a7338739', N'44af6ccc-fc4f-4286-be24-8cc4c2efd567')

");
        }
        
        public override void Down()
        {
        }
    }
}
