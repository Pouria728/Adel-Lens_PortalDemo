using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202601281200)]
	public class _202601281200_AddIsSpecialToLensTypes : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"
IF COL_LENGTH('dbo.Tbl_Lns_LensType', 'IsSpecial') IS NULL
BEGIN
	ALTER TABLE [dbo].[Tbl_Lns_LensType] ADD [IsSpecial] [bit] NOT NULL CONSTRAINT [DF_Tbl_Lns_LensType_IsSpecial] DEFAULT 0;
END;
";

			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
