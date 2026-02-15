using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202601311230)]
	public class _202601311230_AddCustomLensPermissions : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensBrand_Index')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - مشاهده برند', 'CustomLensBrand_Index');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensBrand_Create')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ایجاد برند', 'CustomLensBrand_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensBrand_Edit')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ویرایش برند', 'CustomLensBrand_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensBrand_Delete')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - حذف برند', 'CustomLensBrand_Delete');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensType_Index')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - مشاهده Lens Type', 'CustomLensType_Index');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensType_Create')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ایجاد Lens Type', 'CustomLensType_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensType_Edit')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ویرایش Lens Type', 'CustomLensType_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensType_Delete')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - حذف Lens Type', 'CustomLensType_Delete');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensDesignType_Index')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - مشاهده Design Type', 'CustomLensDesignType_Index');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensDesignType_Create')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ایجاد Design Type', 'CustomLensDesignType_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensDesignType_Edit')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ویرایش Design Type', 'CustomLensDesignType_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensDesignType_Delete')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - حذف Design Type', 'CustomLensDesignType_Delete');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensTypeCoating_Index')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - مشاهده Lens Type & Coating', 'CustomLensTypeCoating_Index');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensTypeCoating_Create')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ایجاد Lens Type & Coating', 'CustomLensTypeCoating_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensTypeCoating_Edit')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ویرایش Lens Type & Coating', 'CustomLensTypeCoating_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensTypeCoating_Delete')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - حذف Lens Type & Coating', 'CustomLensTypeCoating_Delete');
";

			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
