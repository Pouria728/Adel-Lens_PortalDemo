using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202601281230)]
	public class _202601281230_AddIsCorridorToLensTypes : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"
IF COL_LENGTH('dbo.Tbl_Lns_LensType', 'IsCorridor') IS NULL
BEGIN
	ALTER TABLE [dbo].[Tbl_Lns_LensType] ADD [IsCorridor] [bit] NOT NULL CONSTRAINT [DF_Tbl_Lns_LensType_IsCorridor] DEFAULT 0;
END;
";

			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
