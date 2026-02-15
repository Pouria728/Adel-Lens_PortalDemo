using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211211)]
	public class _202508211211_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"	
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
if not exists(select * from Roles)
INSERT [dbo].[Roles] ([RoleId], [RoleName], [TenantId], [RoleKey]) VALUES (1, N'مدیر سیستم', 1, NULL)

GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
	SET IDENTITY_INSERT [dbo].[Users] ON 
GO
if not exists(select * from users)
INSERT [dbo].[Users] ([UserId], [Username], [DisplayName], [Email], [Source], [PasswordHash], [PasswordSalt], [LastDirectoryUpdate], [UserImage], [InsertDate], [InsertUserId], [UpdateDate], [UpdateUserId], [IsActive], [FirstName], [LastName], [NationalCode], [Mobile], [WinId], [TenantId], [CompanyId], [FiscalYear], [MobilePhoneNumber], [MobilePhoneVerified], [TwoFactorAuth], [InfoCustomerId], [AllowForceSend], [IsAdmin]) 
VALUES (1, N'admin', N'admin', N'admin@dummy.com', N'site', N'NCzsQDPPqys=', N'316f0547-01df-4a86-a556-86bff846f1c7', NULL, NULL, CAST(N'2014-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1, N'admin', N'admin', NULL, NULL, NULL, 1, 3, N'1403/01/01', NULL, 0, NULL, NULL, 0, 1)
		GO
SET IDENTITY_INSERT [dbo].[Users] Off
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 
GO
if not exists(select * from UserRoles)
INSERT [dbo].[UserRoles] ([UserRoleId], [UserId], [RoleId]) VALUES (1, 1, 1)

GO
SET IDENTITY_INSERT [dbo].[UserRoles] OFF




				";
			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
