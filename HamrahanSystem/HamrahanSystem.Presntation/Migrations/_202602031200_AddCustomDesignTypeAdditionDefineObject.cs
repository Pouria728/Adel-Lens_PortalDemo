using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [Migration(202602031200)]
    public class _202602031200_AddCustomDesignTypeAdditionDefineObject : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
IF COL_LENGTH('dbo.Tbl_Lns_CustomDesignTypeAddition', 'DefineObjectId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_CustomDesignTypeAddition]
        ADD [DefineObjectId] INT NULL;
END");
        }

        public override void Down()
        {
            Execute.Sql(@"
IF COL_LENGTH('dbo.Tbl_Lns_CustomDesignTypeAddition', 'DefineObjectId') IS NOT NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_CustomDesignTypeAddition]
        DROP COLUMN [DefineObjectId];
END");
        }
    }
}
