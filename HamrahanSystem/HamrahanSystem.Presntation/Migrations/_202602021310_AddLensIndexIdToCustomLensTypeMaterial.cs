using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602021310)]
    public class _202602021310_AddLensIndexIdToCustomLensTypeMaterial : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF COL_LENGTH('dbo.Tbl_Lns_CustomLensTypeMaterial', 'LensIndexId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_CustomLensTypeMaterial]
    ADD [LensIndexId] INT NULL;
END;
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
