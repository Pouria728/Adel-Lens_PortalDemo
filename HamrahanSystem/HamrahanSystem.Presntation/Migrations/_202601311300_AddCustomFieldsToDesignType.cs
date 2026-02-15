using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202601311300)]
	public class _202601311300_AddCustomFieldsToDesignType : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"
IF COL_LENGTH('dbo.Tbl_Lns_DesignType', 'IsSpecial') IS NULL
BEGIN
	ALTER TABLE [dbo].[Tbl_Lns_DesignType] ADD [IsSpecial] [bit] NOT NULL CONSTRAINT [DF_Tbl_Lns_DesignType_IsSpecial] DEFAULT 0;
END;
IF COL_LENGTH('dbo.Tbl_Lns_DesignType', 'LensTypeId') IS NULL
BEGIN
	ALTER TABLE [dbo].[Tbl_Lns_DesignType] ADD [LensTypeId] [int] NULL;
END;
";

			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
