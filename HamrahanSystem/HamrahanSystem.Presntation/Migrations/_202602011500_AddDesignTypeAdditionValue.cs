using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011500)]
    public class _202602011500_AddDesignTypeAdditionValue : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF COL_LENGTH('dbo.Tbl_Lns_DesignType', 'AdditionValue') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_DesignType] ADD [AdditionValue] [decimal](18,2) NULL;
END;
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
