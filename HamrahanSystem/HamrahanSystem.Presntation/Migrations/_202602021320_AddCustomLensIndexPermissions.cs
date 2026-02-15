using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602021320)]
    public class _202602021320_AddCustomLensIndexPermissions : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensIndex_Index')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - مشاهده Lens Index', 'CustomLensIndex_Index');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensIndex_Create')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ایجاد Lens Index', 'CustomLensIndex_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensIndex_Edit')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - ویرایش Lens Index', 'CustomLensIndex_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensIndex_Delete')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - حذف Lens Index', 'CustomLensIndex_Delete');
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
