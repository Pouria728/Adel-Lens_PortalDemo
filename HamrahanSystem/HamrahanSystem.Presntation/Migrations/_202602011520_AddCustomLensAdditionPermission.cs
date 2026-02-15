using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011520)]
    public class _202602011520_AddCustomLensAdditionPermission : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'CustomLensAddition_Index')
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey]) VALUES (N'سفارشی - مشاهده ادیشن', 'CustomLensAddition_Index');
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
