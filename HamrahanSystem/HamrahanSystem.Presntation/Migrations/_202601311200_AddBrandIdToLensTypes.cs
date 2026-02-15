using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202601311200)]
	public class _202601311200_AddBrandIdToLensTypes : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"
IF COL_LENGTH('dbo.Tbl_Lns_LensType', 'BrandId') IS NULL
BEGIN
	ALTER TABLE [dbo].[Tbl_Lns_LensType] ADD [BrandId] [int] NULL;
END;
";

			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
