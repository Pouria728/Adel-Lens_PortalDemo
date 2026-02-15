using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602071400)]
    public class _202602071400_AddCustomLensSphCylPermissions : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensSph_Index')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'Custom - Sph View', 'CustomLensSph_Index');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensSph_Create')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'Custom - Sph Create', 'CustomLensSph_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensSph_Edit')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'Custom - Sph Edit', 'CustomLensSph_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensSph_Delete')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'Custom - Sph Delete', 'CustomLensSph_Delete');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensCyl_Index')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'Custom - Cyl View', 'CustomLensCyl_Index');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensCyl_Create')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'Custom - Cyl Create', 'CustomLensCyl_Create');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensCyl_Edit')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'Custom - Cyl Edit', 'CustomLensCyl_Edit');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensCyl_Delete')
    INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'Custom - Cyl Delete', 'CustomLensCyl_Delete');
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
