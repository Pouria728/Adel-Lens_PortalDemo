using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011740)]
    public class _202602011740_AddCustomLensTypeMaterialPermissions : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensTypeMaterial_Index')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - مشاهده Material', 'CustomLensTypeMaterial_Index');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensTypeMaterial_Create')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ایجاد Material', 'CustomLensTypeMaterial_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensTypeMaterial_Edit')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ویرایش Material', 'CustomLensTypeMaterial_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensTypeMaterial_Delete')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - حذف Material', 'CustomLensTypeMaterial_Delete');
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
