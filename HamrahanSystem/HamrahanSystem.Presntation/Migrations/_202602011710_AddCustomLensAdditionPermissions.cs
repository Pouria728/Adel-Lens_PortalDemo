using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011710)]
    public class _202602011710_AddCustomLensAdditionPermissions : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensAddition_Create')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ایجاد ادیشن', 'CustomLensAddition_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensAddition_Edit')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ویرایش ادیشن', 'CustomLensAddition_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensAddition_Delete')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - حذف ادیشن', 'CustomLensAddition_Delete');
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
