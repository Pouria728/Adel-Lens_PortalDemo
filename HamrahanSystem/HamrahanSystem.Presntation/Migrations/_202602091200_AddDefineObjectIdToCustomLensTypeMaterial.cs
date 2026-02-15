using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602091200)]
    public class _202602091200_AddDefineObjectIdToCustomLensTypeMaterial : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF COL_LENGTH('dbo.Tbl_Lns_CustomLensTypeMaterial', 'DefineObjectId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_CustomLensTypeMaterial]
        ADD [DefineObjectId] INT NULL;
END;
";
            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
