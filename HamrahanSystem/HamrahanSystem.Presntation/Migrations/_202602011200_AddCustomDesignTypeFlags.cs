using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011200)]
    public class _202602011200_AddCustomDesignTypeFlags : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF COL_LENGTH('dbo.Tbl_Lns_DesignType', 'SphPlus') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_DesignType] ADD [SphPlus] [bit] NOT NULL CONSTRAINT [DF_Tbl_Lns_DesignType_SphPlus] DEFAULT 0;
END;
IF COL_LENGTH('dbo.Tbl_Lns_DesignType', 'SphMinus') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_DesignType] ADD [SphMinus] [bit] NOT NULL CONSTRAINT [DF_Tbl_Lns_DesignType_SphMinus] DEFAULT 0;
END;
IF COL_LENGTH('dbo.Tbl_Lns_DesignType', 'Addition') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_DesignType] ADD [Addition] [bit] NOT NULL CONSTRAINT [DF_Tbl_Lns_DesignType_Addition] DEFAULT 0;
END;
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
